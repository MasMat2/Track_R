import { HubConnectionState, HubConnection, IHttpConnectionOptions, HttpTransportType, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { BehaviorSubject, filter, take, timeout, catchError } from "rxjs";
import { environment } from "src/environments/environment";
import { AuthService } from "src/app/auth/auth.service";

export class SignalingHubBase extends EventTarget {
  protected connectionStatus = new BehaviorSubject<HubConnectionState>(HubConnectionState.Disconnected);

  protected connection: HubConnection;

  
  private messaageSource = new BehaviorSubject<string>('');
  message$ = this.messaageSource.asObservable();
  public peer_id: string;

  constructor(
    protected endpoint: string,
    private authService: AuthService
  ) {
    super();
    this.iniciarConexion();
  }

  public async iniciarConexion() {
    const token: string | null = await this.authService.obtenerToken();



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
      'NewMessage',
      (json_string: string) => this.onNewMessage(json_string)
    );

    this.connection.on(
        'LocalId',
        (local_id: string) => this.onLocalId(local_id)
    );

    this.connection.on(
        'PeerId',
        (peer_id: string) => this.onPeerId(peer_id)
    );

    this.connectionStatus.next(HubConnectionState.Connecting);

    await this.connection.start();
  }

  
  public async crearLlamada(id?: string) {
    
    await this.ensureConnection();

    console.log(`CrearLlamada: ${id}`);

    if(id != null){
        this.peer_id = id;
    }
    
    return await this.connection.invoke('CrearLlamada', id);

  }

  public async sendMessage(obj: any) {
    await this.ensureConnection();
    
    await this.connection.invoke('EnviarMensaje', this.peer_id, JSON.stringify(obj));

  }
  
  protected onNewMessage(json_string: any){
    this.messaageSource.next(json_string);
  };

  protected onLocalId(local_id: any){
    var json_string = JSON.stringify({
        type: "local-id",
        local_id: local_id
    });
    this.onNewMessage(json_string);
  };

  protected onPeerId(peer_id: any){
    this.peer_id = peer_id;
    console.log(`PeerId: ${peer_id}`);
    this.dispatchEvent(new Event('peerconnected'));
  };

  public async detenerConexion() {
    this.connectionStatus.next(HubConnectionState.Disconnecting);

    await this.connection.stop();

    this.connectionStatus.next(HubConnectionState.Disconnected);
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