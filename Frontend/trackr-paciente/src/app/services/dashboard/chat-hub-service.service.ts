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
import { Constants } from '@utils/constants/constants';
import { ChatPersonaService } from '@http/chat/chat-persona.service';
import { ChatDTO } from 'src/app/shared/Dtos/Chat/chat-dto';
import { ChatPersonaFormDTO } from 'src/app/shared/Dtos/Chat/chat-persona-form-dto';
import { AuthService } from 'src/app/auth/auth.service';
import { FechaService } from '@services/fecha.service';

@Injectable({
  providedIn: 'root',
})
export class ChatHubServiceService {
  private connectionStatus = new BehaviorSubject<HubConnectionState>(
    HubConnectionState.Disconnected
  );

  private chatSubject = new BehaviorSubject<ChatDTO[]>([]);
  public chat$ = this.chatSubject.asObservable().pipe(
    map((chats) => {
      return chats.map(chat => {
        if(!chat.fechaYaFormateada){
          chat.fecha = this.fechaService.fechaUTCAFechaLocal(chat.fecha);
          chat.fechaYaFormateada = true;
        }
        return chat;
      });
    })
  );

  private readonly endpoint = 'hub/chat';

  private connection: HubConnection;

  constructor(
    private ChatPersonaService: ChatPersonaService,
    private authService: AuthService,
    private fechaService: FechaService
  ) {
    this.iniciarConexion();
    console.log('Iniciando conexion con el Hub de Chat...');
  }

  public async iniciarConexion() {
    console.log('[ChatHubService] Iniciando conexión...');
   
    const token = await this.authService.obtenerToken();
    console.log('[ChatHubService] Token obtenido:', token ? 'Token válido' : 'Token no disponible');
   
    if (!token) {
      console.log('[ChatHubService] Conexión cancelada - No hay token');
      return;
    }
   
    const url = `${environment.urlBackend}${this.endpoint}`;
    console.log('[ChatHubService] URL de conexión:', url);
   
    const connectionConfig: IHttpConnectionOptions = {
      accessTokenFactory: () => {
        console.log('[ChatHubService] Generando token para conexión');
        return token;
      },
    };
   
    this.connection = new HubConnectionBuilder()
      .withUrl(url, connectionConfig)
      .build();
    console.log('[ChatHubService] Conexión construida');
   
    this.connection.on('NuevoChat', (chat: ChatDTO, idPersonas: number[]) => {
      console.log('[ChatHubService] NuevoChat recibido:', { chat, idPersonas });
      this.onNuevoChat(chat, idPersonas);
    });
   
    this.connection.on('NuevaConexion', (chats: ChatDTO[]) => {
      console.log('[ChatHubService] NuevaConexion recibida:', chats);
      this.onNuevaConexion(chats);
    });
   
    this.connection.on('CargarChats', (chats: ChatDTO[]) => {
      console.log('[ChatHubService] CargarChats recibido:', chats);
      this.onCargarChats(chats);
    });
   
    this.connectionStatus.next(HubConnectionState.Connecting);
    console.log('[ChatHubService] Estado cambiado a: Connecting');
   
    try {
      await this.connection.start();
      console.log('[ChatHubService] Conexión iniciada exitosamente');
    } catch (error) {
      console.error('[ChatHubService] Error al iniciar conexión:', error);
      throw error;
    }
   }

  public async detenerConexion() {
    this.connectionStatus.next(HubConnectionState.Disconnecting);

    await this.connection.stop();
    this.chatSubject.next([]);
    this.connectionStatus.next(HubConnectionState.Disconnected);
  }

  public obtenerNotificaciones(): ChatDTO[] {
    return this.chatSubject.value;
  }

  private async onNuevoChat(chat:ChatDTO,idPersonas:number[]){

    //no hacer nada de momento
    //const chats = this.chatSubject.value;
    //chats.push(chat);
  }

  private onNuevaConexion(chats: ChatDTO[]): void {
    this.chatSubject.next(chats);
  }

  private onCargarChats(chats:ChatDTO[]):void{
    this.chatSubject.next(chats);
  }

  public async ensureConnection(): Promise<void> {
    const timeoutms = 10_000;

    if (this.connection.state === HubConnectionState.Connected) {
      return;
    } else if (
      this.connection.state === HubConnectionState.Disconnected ||
      this.connection.state === HubConnectionState.Disconnecting
    ) {
      this.iniciarConexion();
      console.log('No se ha iniciado la conexión con el Hub de Notificaciones, Reconectando...');
    } else if (
      this.connection.state === HubConnectionState.Connecting ||
      this.connection.state === HubConnectionState.Reconnecting
    ) {
      this.connectionStatus.asObservable().pipe(
        filter((state) => state === HubConnectionState.Connected),
        take(1),
        timeout(timeoutms),
        catchError(() => {
          throw new Error(
            'No se pudo establecer la conexión con el Hub de Notificaciones'
          );
        })
      );
    }
  }

  public async agregarChat(mensaje: ChatDTO, idPersonas: number[]) {
    await this.ensureConnection();

    await this.connection.invoke('NuevoChat', mensaje, idPersonas);
  }

  public abandonarChat(idChat:number){
    let chats = this.chatSubject.value

    chats = chats.filter(x => x.idChat != idChat)

    this.chatSubject.next(chats);
  }
}
