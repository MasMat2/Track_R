import { Injectable } from '@angular/core';
import {
  HttpTransportType,
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
  IHttpConnectionOptions,
  LogLevel
} from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { catchError, filter, map, take, timeout } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Constants } from '@utils/constants/constants';
import { ChatMensajeDTO } from '@dtos/chats/chat-mensaje-dto';
import { FechaService } from './fecha.service';

@Injectable({
  providedIn: 'root'
})
export class ChatMensajeHubService {
  private connectionStatus = new BehaviorSubject<HubConnectionState>(HubConnectionState.Disconnected);
  
  private chatMensajeSubject = new BehaviorSubject<ChatMensajeDTO[][]>([]);
  //public chatMensaje$ = this.chatMensajeSubject.asObservable();
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
  )

  private readonly endpoint = 'hub/chatMensaje';

  private connection:HubConnection;

  constructor(private fechaService: FechaService) { 
    this.iniciarConexion();
    console.log('Iniciando conexion con el Hub de Chat Mensajes...');
  }

  public async iniciarConexion() {
    console.log('[ChatMensajeHub] Iniciando conexión...');
   
    const token: string | null = localStorage.getItem(Constants.TOKEN_KEY);
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
      }
    };
   
    this.connection = new HubConnectionBuilder()
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
   
    this.connectionStatus.next(HubConnectionState.Connecting);
    console.log('[ChatMensajeHub] Estado cambiado a: Connecting');
   
    try {
      await this.connection.start();
      console.log('[ChatMensajeHub] Conexión iniciada exitosamente');
    } catch (error) {
      console.error('[ChatMensajeHub] Error al iniciar conexión:', error);
      throw error;
    }
   }

  public async detenerConexion() {
    this.connectionStatus.next(HubConnectionState.Disconnecting);

    await this.connection.stop();
    this.chatMensajeSubject.next([]);
    this.connectionStatus.next(HubConnectionState.Disconnected);
  }

  public obtenerMensajes():ChatMensajeDTO[][] {
    return this.chatMensajeSubject.value;
  }

  private onNuevoChatMensaje(chatMensaje:ChatMensajeDTO): void{
    const chatsMensaje = this.chatMensajeSubject.value;
    let chat = chatsMensaje.find(x => x.find(y => y.idChat == chatMensaje.idChat))
    if(chat){
      chat.push(chatMensaje)
    }
    else{
      let aux:ChatMensajeDTO[] = []
      aux.push(chatMensaje)
      chatsMensaje.push(aux)

    }
    
    
    //chatsMensaje.splice(0,0,chatMensaje);
    this.chatMensajeSubject.next(chatsMensaje);
  }

  private onNuevaConexion(chats: ChatMensajeDTO[][]): void{
    this.chatMensajeSubject.next(chats);
  }

  private async ensureConnection(): Promise<void> {
    const timeoutms = 10_000;

    if(this.connection.state === HubConnectionState.Connected){
      return;
    }
    else if (
      this.connection.state === HubConnectionState.Disconnected ||
      this.connection.state === HubConnectionState.Disconnecting
    ) {
      await this.iniciarConexion();
      console.log('No se ha iniciado la conexión con el Hub de Notificaciones, Reconectando...');
    }
    else if(
      this.connection.state === HubConnectionState.Connecting ||
      this.connection.state === HubConnectionState.Reconnecting
    ){
      this.connectionStatus
        .asObservable()
        .pipe(
          filter((state) => state === HubConnectionState.Connected),
          take(1),
          timeout(timeoutms),
          catchError(() => { throw new Error('No se pudo establecer la conexión con el Hub de Notificaciones') })
        )
    }
  }

  public async enviarMensaje(mensaje:ChatMensajeDTO) {
    await this.ensureConnection();

     this.connection.invoke('NuevoMensaje', mensaje);
  }
}
