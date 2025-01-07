import { HubConnectionState, HubConnection, IHttpConnectionOptions, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { BehaviorSubject } from "rxjs";
import { environment } from "src/environments/environment";
import { Constants } from "@utils/constants/constants";

export class SignalingHubBase extends EventTarget{
  protected connectionStatus = new BehaviorSubject<HubConnectionState>(HubConnectionState.Disconnected);

  protected connection: HubConnection;

  private messaageSource = new BehaviorSubject<string>('');
  message$ = this.messaageSource.asObservable();

  private startPromise: Promise<void> | null = null;

  constructor(
    protected endpoint: string
  ) {
    super();
    this.iniciarConexion();
    console.log('Iniciando conexion con el Hub de Signaling...');
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
      }
      // transport: HttpTransportType.LongPolling,
      // TODO: 2023-03-23 -> Revisar los tipos de transporte (Web Socket, Long Polling, Server Sent Events)
    };

    this.connection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Debug)
      .withUrl(url, connectionConfig)
      .build();

    this.connection.on(
      'NewMessage',
      (obj: any) => this.onNewMessage(obj)
    );
    
    this.connection.on(
      'LocalId',
      (local_id: string) => this.onLocalId(local_id)
    );

    this.connectionStatus.next(HubConnectionState.Connecting);

    this.startPromise = this.connection.start().catch(error => {
      this.startPromise = null; // Reset the promise if the start fails
      throw error;
    });
  }

  
  public async crearLlamada(caller_id?: string) {
    
    await this.ensureConnection();    
    await this.connection.invoke('CrearLlamada', caller_id);

  }

  public async sendMessage(obj: any) {
    await this.ensureConnection();
    
    await this.connection.invoke('SendMessageToPeer', JSON.stringify(obj), "");

    
  }


  protected async onNewMessage(obj: any){
    
    this.messaageSource.next(obj.content);
    await this.connection.invoke('AcknowledgeMessage', obj.id);
  };

  protected onLocalId(local_id: any){
    var json_string = JSON.stringify({
        type: "local-id",
        local_id: local_id
    });
    this.messaageSource.next(json_string);
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
      this.iniciarConexion();
      console.log('No se ha iniciado la conexión con el Hub de Signaling, Reconectando...');
    }
    else if (
      this.connection.state === HubConnectionState.Connecting ||
      this.connection.state === HubConnectionState.Reconnecting
    ) {
      
      await this.startPromise;
    }
  }
}