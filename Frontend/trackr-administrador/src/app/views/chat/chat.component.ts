import { Component, OnDestroy } from '@angular/core';
import { ChatDTO } from '@dtos/chats/chat-dto';
import { ChatHubServiceService } from '@services/chat-hub-service.service';
import { Observable, Subject, takeUntil, tap } from 'rxjs';
import { ChatMensajeHubService } from '../../shared/services/chat-mensaje-hub.service';
import { ChatMensajeDTO } from '@dtos/chats/chat-mensaje-dto';
import { ActivatedRoute } from '@angular/router';
import { ChatPersonaService } from '@http/chats/chat-persona.service';
import { SessionService } from '@services/session.service';
import { FechaService } from '@services/fecha.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnDestroy {
  protected chats$: Observable<ChatDTO[]>;
  protected chats:ChatDTO[] = [];
  protected chatMensajes$: Observable<ChatMensajeDTO[][]>
  protected mensajes:ChatMensajeDTO[][] = [];
  protected contenido:string;
  protected idChatSeleccionado: number;
  protected mensajesChatSeleccionado: ChatMensajeDTO[]; 
  protected tituloChatSeleccionado: string;
  protected imagenChatSeleccionado: string;
  protected tipoMimeSeleccionado: string;
  private idUsuario : number | null;
  private unsubscribe$ = new Subject<void>();

  protected clickEnChat = false;

  constructor(
    private ChatHubServiceService:ChatHubServiceService,
    private chatMensajeHubService:ChatMensajeHubService,
    private route:ActivatedRoute,
    private chatPersonaService : ChatPersonaService,
    private sessionService : SessionService,
    private fechaService: FechaService
  ) {
    this.chatPersonaService.idChatPadre$
    .pipe(takeUntil(this.unsubscribe$.asObservable()))
    .subscribe(idChat => {
      this.obtenerChatSeleccionado(idChat);
    });
   }

  async ngOnInit(){
    this.obtenerChats();
    this.idUsuario = this.sessionService.obtenerIdUsuarioSesion();
  }


  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  obtenerId(){
    this.route.params.subscribe(params => {
      const idChat = Number(params['id']);
      if(!isNaN(idChat)){
        this.idChatSeleccionado = idChat;
        this.obtenerChatSeleccionado(idChat)
        this.clickEnChat = true;
      }
    })
  }

  obtenerChats(){
    //this.chats$ = this.ChatHubServiceService.chat$
    this.ChatHubServiceService.chat$.subscribe(res => {
      this.chats = res
      this.obtenerUltimoMensaje();
      this.obtenerMensajes();
    })
  }

  obtenerMensajes(){
    //TODO:no descargar los mensajes hasta entrar al chat
    this.chatMensajes$ = this.chatMensajeHubService.chatMensaje$;
    
    this.chatMensajes$.subscribe(res =>{
      if(this.idChatSeleccionado != undefined){
        this.obtenerChatSeleccionado(this.idChatSeleccionado);
      }
      this.mensajes = res;
      this.obtenerUltimoMensaje();
    })
  }

  enviarMensaje(idChat:number): void{
    let msg: ChatMensajeDTO = {
      fecha: this.fechaService.fechaLocalAFechaUTC(new Date()),
      idChat,
      mensaje: this.contenido,
      idPersona:this.idUsuario as number,
      idArchivo: 0
    }

    this.chatMensajeHubService.enviarMensaje(msg);
  }

  obtenerChatSeleccionado(id:number){

    if(this.chats.length == 0){
      this.obtenerChats();
    }
    this.idChatSeleccionado = id;
    this.mensajesChatSeleccionado = this.mensajes.find(array => array.some(x => x.idChat === this.idChatSeleccionado)) || [];
    let aux = this.chats.find(x => x.idChat == id)
    this.tituloChatSeleccionado = aux?.titulo || "";
    this.imagenChatSeleccionado = aux?.imagenBase64 || "";
    this.tipoMimeSeleccionado = aux?.tipoMime || "";
    this.clickEnChat = true;
  }

  private obtenerUltimoMensaje(): void {
    const ultimoMensaje = this.mensajes.map(
      (arr) =>{ return {mensajes: arr[arr.length - 1]?.mensaje || '', chat: arr[0]?.idChat || 0}}
    );

    const fechaUltimoMensaje = this.mensajes.map(
      (arr) => {
        return {fecha: arr[arr.length - 1]?.fecha || this.fechaService.obtenerFechaActualISOString(), chat: arr[0]?.idChat || 0}
      }
    )
    
    this.chats.map(
      chat => {
        chat.ultimoMensaje = ultimoMensaje.filter(y => y.chat == chat.idChat)[0]?.mensajes || '';
        chat.fechaUltimoMensaje = fechaUltimoMensaje.filter(y => y.chat == chat.idChat)[0]?.fecha || '';
        return chat;
      }
    )
    
    this.chats = this.chats.sort((a, b) => {
      const fechaA = a.fechaUltimoMensaje ? new Date(a.fechaUltimoMensaje).getTime() : 0;
      const fechaB = b.fechaUltimoMensaje ? new Date(b.fechaUltimoMensaje).getTime() : 0;
    
      return fechaB - fechaA;
    });
  }
}
