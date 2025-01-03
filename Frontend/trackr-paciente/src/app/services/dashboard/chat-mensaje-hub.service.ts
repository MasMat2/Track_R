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
import { catchError, filter, map, take, timeout } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ChatMensajeDTO } from 'src/app/shared/Dtos/Chat/chat-mensaje-dto';
import { AuthService } from 'src/app/auth/auth.service';
import { ChatHubServiceService } from './chat-hub-service.service';
import { FechaService } from '@services/fecha.service';

@Injectable({
  providedIn: 'root',
})
export class ChatMensajeHubService {
  private connectionStatus = new BehaviorSubject<HubConnectionState>(
    HubConnectionState.Disconnected
  );

  private chatMensajeSubject = new BehaviorSubject<ChatMensajeDTO[][]>([]);
  public chatMensaje$ = this.chatMensajeSubject.asObservable().pipe(
    map((chats) => {
      return chats.map(chat => {
        return chat.map(mensaje => {
          if (!mensaje.fechaYaFormateada) {
            mensaje.fecha = this.fechaService.fechaUTCAFechaLocal(mensaje.fecha);
            mensaje.fechaYaFormateada = true; // Marca la fecha del mensaje como formateada (así se manejan mensajes entrantes)
          }
          return mensaje;
        });
      });
    })
  );

  private readonly endpoint = 'hub/chatMensaje';

  private connection: HubConnection;
  
  private attempts = 0;
  private isRetrying = false;
  
  constructor(
    private authService: AuthService,
    private chatHub:ChatHubServiceService, private fechaService: FechaService
  ) {
    this.iniciarConexion();
    console.log('Iniciando conexion con el Hub de Chat Mensajes...');
  }

  public async iniciarConexion() {
    console.log('[ChatMensajeHub] Iniciando conexión...');
    
    const token = await this.authService.obtenerToken();
    console.log('[ChatMensajeHub] Token obtenido:', token ? 'Token válido' : 'Token no disponible');
    
    if (!token) {
      console.log('[ChatMensajeHub] Conexión cancelada - No hay token');
      return;
    }
  
    const url = `${environment.urlBackend}${this.endpoint}`;
    console.log('[ChatMensajeHub] URL de conexión:', url);
  
    const connectionConfig: IHttpConnectionOptions = {
      accessTokenFactory: () => {
        console.log('[ChatMensajeHub] Generando token para conexión');
        return token;
      },
    };
  
    this.connection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Debug)
      .withUrl(url, connectionConfig)
      .build();
    console.log('[ChatMensajeHub] Conexión construida');
  
    this.connection.on('NuevoMensaje', (chatMensaje: ChatMensajeDTO) => {
      console.log('[ChatMensajeHub] NuevoMensaje recibido:', chatMensaje);
      this.onNuevoChatMensaje(chatMensaje);
    });
  
    this.connection.on('NuevaConexion', (mensajes: ChatMensajeDTO[][]) => {
      console.log('[ChatMensajeHub] NuevaConexion recibida:', mensajes);
      this.onNuevaConexion(mensajes);
    });
  
    this.connection.on('AbandonarChat', (idChat: number) => {
      console.log('[ChatMensajeHub] AbandonarChat recibido:', idChat);
      this.onAbandonarChat(idChat);
    });
  
    this.connectionStatus.next(HubConnectionState.Connecting);
    console.log('[ChatMensajeHub] Estado cambiado a: Connecting');
  
    this.connection.onclose(async (error) => {
      console.log('[ChatMensajeHub] Conexión cerrada. Error:', error);
      await this.retryConnection();
    });
  
    try {
      await this.connection.start();
      console.log('[ChatMensajeHub] Conexión iniciada exitosamente');
    } catch (error) {
      console.error('[ChatMensajeHub] Error al iniciar conexión:', error);
      throw error;
    }
  }

  private async retryConnection() {
    const maxAttempts = 5;
    if (this.isRetrying) return;
    
    this.isRetrying = true;
    while (this.attempts < maxAttempts) {
      try {
        await this.connection.start();
        console.log('Reconnected!');
        break;
      } catch (err) {
        this.attempts++;
        console.warn(`Retry attempt ${this.attempts} failed.`);
        await this.delay(2000); // Wait a bit before trying again
      }
    }
  }
  
  private delay(ms: number) {
    return new Promise((resolve) => setTimeout(resolve, ms));
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
      await this.iniciarConexion();
      console.log('No se ha iniciado la conexion con el Hub de Notificaciones , Reconectando...');
      return;
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
      await this.iniciarConexion();
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

  
  public async obtenerMensajesDesdeServidor(){
    await this.ensureConnection();

    await this.connection.invoke('ObtenerMensajesDesdeServidor');
  }
}
