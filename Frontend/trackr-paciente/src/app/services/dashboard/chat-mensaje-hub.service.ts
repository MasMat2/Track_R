import { Injectable } from '@angular/core';
import {
  HttpTransportType,
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
  IHttpConnectionOptions,
  LogLevel,
} from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { catchError, filter, take, timeout } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Constants } from '@utils/constants/constants';
import { ChatMensajeDTO } from 'src/app/shared/Dtos/Chat/chat-mensaje-dto';
import { AuthService } from 'src/app/auth/auth.service';
import { ChatHubServiceService } from './chat-hub-service.service';

@Injectable({
  providedIn: 'root',
})
export class ChatMensajeHubService {
  private connectionStatus = new BehaviorSubject<HubConnectionState>(
    HubConnectionState.Disconnected
  );

  private chatMensajeSubject = new BehaviorSubject<ChatMensajeDTO[][]>([]);
  public chatMensaje$ = this.chatMensajeSubject.asObservable();

  private readonly endpoint = 'hub/chatMensaje';

  private connection: HubConnection;

  constructor(private authService: AuthService,
              private chatHub:ChatHubServiceService) {
    this.iniciarConexion();
  }

  public async iniciarConexion() {
    const token = await this.authService.obtenerToken();

    if (!token) {
      return;
    }

    const url = `${environment.urlBackend}${this.endpoint}`;

    const connectionConfig: IHttpConnectionOptions = {
      accessTokenFactory: () => {
        return token;
      },
      transport: HttpTransportType.LongPolling,
    };

    this.connection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Debug)
      .withUrl(url, connectionConfig)
      .build();

    this.connection.on('NuevoMensaje', (chatMensaje: ChatMensajeDTO) =>
      this.onNuevoChatMensaje(chatMensaje)
    );

    this.connection.on('NuevaConexion', (mensajes: ChatMensajeDTO[][]) =>
      this.onNuevaConexion(mensajes)
    );

    this.connection.on('AbandonarChat', (idChat: number) => this.onAbandonarChat(idChat));

    this.connectionStatus.next(HubConnectionState.Connecting);

    await this.connection.start();
  }

  public async detenerConexion() {
    this.connectionStatus.next(HubConnectionState.Disconnecting);

    await this.connection.stop();
    this.chatMensajeSubject.next([]);
    this.connectionStatus.next(HubConnectionState.Disconnected);
  }

  public obtenerMensajes(): ChatMensajeDTO[][] {
    return this.chatMensajeSubject.value;
  }

  private onNuevoChatMensaje(chatMensaje: ChatMensajeDTO): void {
    const chatsMensaje = this.chatMensajeSubject.value;
    let chat = chatsMensaje.find((x) =>
      x.find((y) => y.idChat == chatMensaje.idChat)
    );
    if (chat) {
      chat.push(chatMensaje);
    } else {
      let aux: ChatMensajeDTO[] = [];
      aux.push(chatMensaje);
      chatsMensaje.push(aux);
    }

    //chatsMensaje.splice(0,0,chatMensaje);
    this.chatMensajeSubject.next(chatsMensaje);
  }

  private onNuevaConexion(chats: ChatMensajeDTO[][]): void {
    this.chatMensajeSubject.next(chats);
  }

  private onAbandonarChat(idChat:number){
    let mensajes = this.chatMensajeSubject.value

    mensajes.forEach( (mensaje,indice) => {
      if(mensaje.some(x => x.idChat == idChat)){
        mensajes.splice(indice,1);
      }
    })

    this.chatMensajeSubject.next(mensajes);
    this.chatHub.abandonarChat(idChat);
  }

  private async ensureConnection(): Promise<void> {
    const timeoutms = 10_000;
    const token = await this.authService.obtenerToken();

    if (!token) {
        throw new Error('No se encontró un token válido');
    }

    if (this.connection.state === HubConnectionState.Connected) {
      return;
    } else if (
      this.connection.state === HubConnectionState.Disconnected ||
      this.connection.state === HubConnectionState.Disconnecting
    ) {
      throw new Error(
        'No se ha iniciado la conexion con el Hub de Notificaciones'
      );
    } else if (
      this.connection.state === HubConnectionState.Connecting ||
      this.connection.state === HubConnectionState.Reconnecting
    ) {
        await this.connectionStatus.asObservable().pipe(
        filter((state) => state === HubConnectionState.Connected),
        take(1),
        timeout(timeoutms),
        catchError(() => {
          throw new Error(
            'No se pudo establecer la conexión con el Hub de Notificaciones'
          );
        })
        ).toPromise();
    } else {
        // Intentar reconectar si el estado es distinto a Connected
      this.iniciarConexion();
    }
}
  public async enviarMensaje(mensaje: ChatMensajeDTO) {
    await this.ensureConnection();

    await this.connection.invoke('NuevoMensaje', mensaje);
    
  }

  public async abandonarChat(idChat:number){
    await this.ensureConnection();

    await this.connection.invoke('AbandonarChat',idChat);
  }
}
