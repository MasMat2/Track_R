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
import { catchError, filter, take, timeout } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Constants } from '@utils/constants/constants';
import { ChatDTO } from '@dtos/chats/chat-dto';
import { ChatPersonaService } from '../http/chats/chat-persona.service';
import { ChatPersonaFormDTO } from '../dtos/chats/chat-persona-form-dto';
import { FechaService } from './fecha.service';

@Injectable({
  providedIn: 'root'
})
export class ChatHubServiceService {
  private connectionStatus = new BehaviorSubject<HubConnectionState>(HubConnectionState.Disconnected);
  
  private chatSubject = new BehaviorSubject<ChatDTO[]>([]);
  public chat$ = this.chatSubject.asObservable();

  private readonly endpoint = 'hub/chat';

  private connection:HubConnection;

  constructor(private ChatPersonaService:ChatPersonaService, private fechaService: FechaService) { 
    this.iniciarConexion();
    console.log('Iniciando conexion con el Hub de Chat...');
  }

  public async iniciarConexion(){
    const token: string | null = localStorage.getItem(Constants.TOKEN_KEY);

    if(!token){
      return;
    }

    const url = `${environment.urlBackend}${this.endpoint}`

    const connectionConfig: IHttpConnectionOptions = {
      accessTokenFactory: () => {
        return token;
      }
      // transport: HttpTransportType.LongPolling
    };

    this.connection = new HubConnectionBuilder()
      // .configureLogging(LogLevel.Debug)
      .withUrl(url, connectionConfig)
      .build();
    
    this.connection.on(
      'NuevoChat',
      (chat: ChatDTO,idPersonas:number[]) => this.onNuevoChat(chat,idPersonas)
    );

    this.connection.on(
      'NuevaConexion',
      (chats: ChatDTO[]) => this.onNuevaConexion(chats)
    );

    this.connection.on(
      'CargarChats',
      (chats: ChatDTO[]) => this.onCargarChats(chats)
    )

    this.connection.on('EliminarChat', (idChat:number) => this.onEliminarChat(idChat));

    this.connectionStatus.next(HubConnectionState.Connecting);

    await this.connection.start();
  }

  public async detenerConexion() {
    this.connectionStatus.next(HubConnectionState.Disconnecting);

    await this.connection.stop();
    this.chatSubject.next([]);
    this.connectionStatus.next(HubConnectionState.Disconnected);
  }

  public obtenerNotificaciones():ChatDTO[] {
    return this.chatSubject.value;
  }

  private onNuevoChat(chat:ChatDTO,idPersonas:number[]): void{
    chat.fecha = this.fechaService.fechaLocalAFechaUTC(new Date());

    const chats = this.chatSubject.value;
    chats.push(chat);
    this.chatSubject.next(chats);
    /*chat.fecha = new Date();

    const chats = this.chatSubject.value;
    chats.push(chat);
    
    let chatPersona: ChatPersonaFormDTO = {
      idPersonas: idPersonas,
      idChat: chat.idChat || 0,
      idTipo: 2
    }

    this.ChatPersonaService.agregarPersonas(chatPersona).subscribe(res => {
      this.chatSubject.next(chats);
      this.connection.invoke('NuevaConexion',chats)
    })*/
  }

  private onNuevaConexion(chats: ChatDTO[]): void{
    for(const chat of chats){
      chat.fecha = this.fechaService.fechaUTCAFechaLocal(chat.fecha);
    }

    this.chatSubject.next(chats);
  }

  private onEliminarChat(idChat:number){
    let chats = this.chatSubject.value

    chats = chats.filter(x => x.idChat != idChat)

    this.chatSubject.next(chats);
  }

  private onCargarChats(chats:ChatDTO[]):void{
    this.chatSubject.next(chats)
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
      this.iniciarConexion();
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

  public async agregarChat(mensaje:ChatDTO,idPersonas:number[]) {
    await this.ensureConnection();

    await this.connection.invoke('NuevoChat', mensaje,idPersonas);
  }

  public async eliminarChat(idChat:number){
    await this.ensureConnection();
    await this.connection.invoke('EliminarChat', idChat);
  }
}
