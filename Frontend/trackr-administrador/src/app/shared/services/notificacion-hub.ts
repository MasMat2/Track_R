import { HubConnectionState, HubConnection, IHttpConnectionOptions, HttpTransportType, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { Constants } from "@utils/constants/constants";
import { BehaviorSubject, filter, take, timeout, catchError, map } from "rxjs";
import { environment } from "src/environments/environment";
import { NotificacionUsuarioBaseDTO } from '../dtos/notificaciones/notificacion-usuario-base-dto';

export class NotificacionHubBase<T extends NotificacionUsuarioBaseDTO> {
  protected connectionStatus = new BehaviorSubject<HubConnectionState>(HubConnectionState.Disconnected);

  protected notificacionesSubject = new BehaviorSubject<T[]>([]);
  public notificaciones$ = this.notificacionesSubject.asObservable()

  protected connection: HubConnection;

  constructor(
    protected endpoint: string
  ) {
    this.iniciarConexion();
    console.log('Iniciando conexion con el Hub de Notificaciones...');
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
      // transport: HttpTransportType.LongPolling,
      // TODO: 2023-03-23 -> Revisar los tipos de transporte (Web Socket, Long Polling, Server Sent Events)
    };

    this.connection = new HubConnectionBuilder()
      .configureLogging(LogLevel.None)
      .withUrl(url, connectionConfig)
      .build();

    this.connection.on(
      'NuevaNotificacion',
      (notificacion: T) => this.onNuevaNotificacion(notificacion)
    );

    this.connection.on(
      'NuevaConexion',
      (notificaciones: T[]) => this.onNuevaConexion(notificaciones)
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
    this.notificacionesSubject.next([]);

    this.connectionStatus.next(HubConnectionState.Disconnected);
  }

  public obtenerNotificaciones(): T[] {
    return this.notificacionesSubject.value;
  }

  public async marcarComoVista(id: number, tomaTomada: boolean = true) {
    this.marcarComoVistas([id], tomaTomada);
  }

  public async marcarComoVistas(ids: number[], tomaTomada: boolean = true) {
    await this.ensureConnection();

    await this.connection.invoke('MarcarComoVistas', ids, tomaTomada);
  }

  public async marcarTodasComoVistas() {
    const ids = this.obtenerNotificacionesNoVistas();
    this.marcarComoVistas(ids);
  }

  protected obtenerNotificacionesNoVistas() {
    const notificaciones = this.notificacionesSubject.value;
    const ids = notificaciones
      .filter((n) => n.visto == false)
      .map((n) => n.idNotificacionUsuario);

    return ids;
  }

  protected onNuevaNotificacion(notificacion: T): void {
    //notificacion.fechaAlta = new Date(notificacion.fechaAlta);

    const notificaciones = this.notificacionesSubject.value;
    notificaciones.splice(0, 0, notificacion);

    this.notificacionesSubject.next(notificaciones);
  }

  protected onNuevaConexion(notificaciones: T[]): void {
    for (const notificacion of notificaciones) {
      //notificacion.fechaAlta = new Date(notificacion.fechaAlta);
    }

    this.notificacionesSubject.next(notificaciones);
  }

  protected onNotificarComoVistas(ids: number[]): void {
    const notificaciones = this.notificacionesSubject.value.filter((n) =>
      ids.includes(n.idNotificacionUsuario)
    );

    if (notificaciones.length == 0) {
      return;
    }

    for (const notificacion of notificaciones) {
      // TODO: 2023-08-21 -> Temporal
      notificacion.visto = !notificacion.visto;
    }

    this.notificacionesSubject.next(this.notificacionesSubject.value);
  }

  public limpiarNotificaciones(): void {
    this.notificacionesSubject.next([]);
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
      this.iniciarConexion();
      console.log('No se ha iniciado la conexion con el Hub de Notificaciones , Reconectando...');
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
