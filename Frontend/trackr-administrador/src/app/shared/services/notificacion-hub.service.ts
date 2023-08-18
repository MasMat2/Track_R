import { Injectable } from '@angular/core';
import {
  HttpTransportType,
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
  IHttpConnectionOptions,
  LogLevel
} from '@microsoft/signalr';
import { NotificacionUsuarioDTO } from '@dtos/notificaciones/notificacion-usuario-dto';
import { BehaviorSubject } from 'rxjs';
import { catchError, filter, take, timeout } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Constants } from '@utils/constants/constants';

@Injectable({
  providedIn: 'root',
})
export class NotificacionHubService {
  private connectionStatus = new BehaviorSubject<HubConnectionState>(HubConnectionState.Disconnected);

  private notificacionSubject = new BehaviorSubject<NotificacionUsuarioDTO[]>([]);
  public notificacion$ = this.notificacionSubject.asObservable();

  private readonly endpoint = 'hub/notificacion';

  private connection: HubConnection;

  constructor() {
    this.iniciarConexion();
  }

  public async iniciarConexion() {
    const token: string | null = localStorage.getItem(Constants.TOKEN_KEY);

    if (!token) {
      return;
    }

    const url = `${environment.urlBackend}${this.endpoint}`;

    const connectionConfig: IHttpConnectionOptions = {
      accessTokenFactory: () => {
        return token;
      },
      transport: HttpTransportType.LongPolling,
      // TODO: 2023-03-23 -> Revisar los tipos de transporte (Web Socket, Long Polling, Server Sent Events)
    };

    this.connection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Debug)
      .withUrl(url, connectionConfig)
      .build();

    this.connection.on(
      'NuevaNotificacion',
      (notificacion: NotificacionUsuarioDTO) => this.onNuevaNotificacion(notificacion)
    );

    this.connection.on(
      'NuevaConexion',
      (notificaciones: NotificacionUsuarioDTO[]) => this.onNuevaConexion(notificaciones)
    );

    this.connection.on(
      'NotificarComoVistas',
      (ids: number[]) => this.onNotificarComoVistas(ids)
    );

    this.connectionStatus.next(HubConnectionState.Connecting);

    await this.connection.start();
  }

  public async detenerConexion() {
    this.connectionStatus.next(HubConnectionState.Disconnecting);

    await this.connection.stop();
    this.notificacionSubject.next([]);

    this.connectionStatus.next(HubConnectionState.Disconnected);
  }

  public obtenerNotificaciones(): NotificacionUsuarioDTO[] {
    return this.notificacionSubject.value;
  }

  public async marcarComoVista(id: number) {
    this.marcarComoVistas([id]);
  }

  public async marcarComoVistas(ids: number[]) {
    await this.ensureConnection();

    await this.connection.invoke('MarcarComoVistas', ids);
  }

  public async marcarTodasComoVistas() {
    const ids = this.obtenerNotificacionesNoVistas();
    this.marcarComoVistas(ids);
  }

  private obtenerNotificacionesNoVistas() {
    const notificaciones = this.notificacionSubject.value;
    const ids = notificaciones
      .filter((n) => n.visto == false)
      .map((n) => n.idNotificacionUsuario);

    return ids;
  }

  private onNuevaNotificacion(notificacion: NotificacionUsuarioDTO): void {
    notificacion.fechaAlta = new Date(notificacion.fechaAlta);

    const notificaciones = this.notificacionSubject.value;
    notificaciones.splice(0, 0, notificacion);

    this.notificacionSubject.next(notificaciones);
  }

  private onNuevaConexion(notificaciones: NotificacionUsuarioDTO[]): void {
    for (const notificacion of notificaciones) {
      notificacion.fechaAlta = new Date(notificacion.fechaAlta);
    }

    this.notificacionSubject.next(notificaciones);
  }

  private onNotificarComoVistas(ids: number[]): void {
    const notificaciones = this.notificacionSubject.value.filter((n) =>
      ids.includes(n.idNotificacionUsuario)
    );

    if (notificaciones.length == 0) {
      return;
    }

    for (const notificacion of notificaciones) {
      notificacion.visto = true;
    }

    this.notificacionSubject.next(this.notificacionSubject.value);
  }

  private async ensureConnection(): Promise<void> {
    const timeoutms = 10_000;

    if (this.connection.state === HubConnectionState.Connected) {
      return;
    }
    else if (
      this.connection.state === HubConnectionState.Disconnected ||
      this.connection.state === HubConnectionState.Disconnecting
    ) {
      throw new Error('No se ha iniciado la conexión con el Hub de Notificaciones');
    }
    else if (
      this.connection.state === HubConnectionState.Connecting ||
      this.connection.state === HubConnectionState.Reconnecting
    ) {
      this.connectionStatus
        .asObservable()
        .pipe(
          filter((state) => state === HubConnectionState.Connected),
          take(1),
          timeout(timeoutms),
          catchError(() => { throw new Error('No se pudo establecer la conexión con el Hub de Notificaciones') })
        );
    }
  }
}
