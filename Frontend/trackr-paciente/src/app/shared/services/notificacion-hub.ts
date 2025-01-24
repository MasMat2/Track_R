
import { NotificacionUsuarioBaseDTO } from "../Dtos/notificaciones/notificacion-usuario-base-dto";
import { HubConnectionState, HubConnection, IHttpConnectionOptions, HttpTransportType, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { Constants } from "@utils/constants/constants";
import { BehaviorSubject, filter, take, timeout, catchError } from "rxjs";
import { environment } from "src/environments/environment";
import { StorageService } from "./storage.service";
import { AuthService } from "src/app/auth/auth.service";

export class NotificacionHubBase<T extends NotificacionUsuarioBaseDTO> {
  protected connectionStatus = new BehaviorSubject<HubConnectionState>(HubConnectionState.Disconnected);

  protected notificacionesSubject = new BehaviorSubject<T[]>([]);
  public notificaciones$ = this.notificacionesSubject.asObservable();

  protected connection: HubConnection;

  constructor(
    protected endpoint: string,
    private authService : AuthService 
  ) {
    this.iniciarConexion();
    console.log('Iniciando conexion con el Hub de Notificaciones...');
  }

  public async iniciarConexion() {
    console.log('[NotificacionHub] Iniciando conexión...');
   
    const token: string | null = await this.authService.obtenerToken();
    console.log('[NotificacionHub] Token obtenido:', token ? 'Token válido' : 'Token no disponible');
   
    if (!token) {
      console.log('[NotificacionHub] Conexión cancelada - No hay token');
      return;
    }
   
    const url = `${environment.urlBackend}${this.endpoint}`;
    console.log('[NotificacionHub] URL de conexión:', url);
   
    const connectionConfig: IHttpConnectionOptions = {
      accessTokenFactory: () => {
        console.log('[NotificacionHub] Generando token para conexión');
        return token;
      },
    };
   
    this.connection = new HubConnectionBuilder()
      .withUrl(url, connectionConfig)
      .build();
    console.log('[NotificacionHub] Conexión construida');
   
    this.connection.on('NuevaNotificacion', (notificacion: T) => {
      console.log('[NotificacionHub] Nueva notificación recibida:', notificacion);
      this.onNuevaNotificacion(notificacion);
    });
   
    this.connection.on('NuevaConexion', (notificaciones: T[]) => {
      console.log('[NotificacionHub] Nueva conexión recibida:', notificaciones);
      this.onNuevaConexion(notificaciones);
    });
   
    this.connection.on('NotificarComoVistas', (ids: number[]) => {
      console.log('[NotificacionHub] Notificaciones marcadas como vistas:', ids);
      this.onNotificarComoVistas(ids);
    });
   
    this.connectionStatus.next(HubConnectionState.Connecting);
    console.log('[NotificacionHub] Estado cambiado a: Connecting');
   
    try {
      await this.connection.start();
      console.log('[NotificacionHub] Conexión iniciada exitosamente');
    } catch (error) {
      console.error('[NotificacionHub] Error al iniciar conexión:', error);
      throw error;
    }
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

  public async marcarComoVista(id: number, tomaTomada : boolean = true) {
    this.marcarComoVistas([id] , tomaTomada);
  }

  public async marcarComoVistas(ids: number[], tomaTomada : boolean = true) {
    await this.ensureConnection();

    await this.connection.invoke('MarcarComoVistas', ids, tomaTomada);
  }

  public async marcarTodasComoVistas() {
    const ids = this.obtenerNotificacionesNoVistas();
    this.marcarComoVistas(ids);
  }

  public obtenerNotificacionesNoVistas() {
    const notificaciones = this.notificacionesSubject.value;
   
    const ids = notificaciones
      .filter((n) => n.visto == false)
      .map((n) => n.idNotificacionUsuario);

    return ids;
  }

  protected onNuevaNotificacion(notificacion: T): void {
    notificacion.fechaAlta = new Date(notificacion.fechaAlta);

    const notificaciones = this.notificacionesSubject.value;
    notificaciones.splice(0, 0, notificacion);

    this.notificacionesSubject.next(notificaciones);
  }

  protected onNuevaConexion(notificaciones: T[]): void {
    for (const notificacion of notificaciones) {
      notificacion.fechaAlta = new Date(notificacion.fechaAlta);
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



  public async ensureConnection(): Promise<void> {
    const timeoutms = 10_000;

    if (this.connection.state === HubConnectionState.Connected) {
      return;
    }
    else if (
      this.connection.state === HubConnectionState.Disconnected ||
      this.connection.state === HubConnectionState.Disconnecting
    ) {
      this.iniciarConexion();
      console.log('No se ha iniciado la conexión con el Hub de Notificaciones, Reconectando...');
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
