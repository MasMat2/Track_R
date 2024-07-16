import { Component, OnDestroy } from '@angular/core';
import { ChatDTO } from '@dtos/chats/chat-dto';
import { ChatHubServiceService } from '@services/chat-hub-service.service';
import { Observable, Subject, takeUntil, tap } from 'rxjs';
import { ChatMensajeHubService } from '../../shared/services/chat-mensaje-hub.service';
import { ChatMensajeDTO } from '@dtos/chats/chat-mensaje-dto';
import { ActivatedRoute } from '@angular/router';
import { ChatPersonaService } from '@http/chats/chat-persona.service';

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
  private unsubscribe$ = new Subject<void>();

  protected clickEnChat = false;

  constructor(
    private ChatHubServiceService:ChatHubServiceService,
    private chatMensajeHubService:ChatMensajeHubService,
    private route:ActivatedRoute,
    private chatPersonaService : ChatPersonaService
  ) {
    this.chatPersonaService.idChatPadre$
    .pipe(takeUntil(this.unsubscribe$.asObservable()))
    .subscribe(idChat => {
      this.obtenerChatSeleccionado(idChat);
    });
   }

  ngOnInit(): void {
    this.obtenerChats();
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
      this.chats = res;
      this.obtenerUltimoMensaje();
      this.obtenerMensajes();
    })
  }

  obtenerMensajes(){
    //TODO:no descargar los mensajes hasta entrar al chat
    //this.chatMensajes$ = this.chatMensajeHubService.chatMensaje$;
    
    this.chatMensajeHubService.chatMensaje$.subscribe(res =>{
      this.mensajes = res;
      this.obtenerUltimoMensaje();
    })
  }

  enviarMensaje(idChat:number): void{
    let msg: ChatMensajeDTO = {
      fecha: new Date(),
      idChat,
      mensaje: this.contenido,
      idPersona:5333,
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

  obtenerUltimoMensaje():void{
    let ultimoMensaje = this.mensajes.map(arr => arr[arr.length - 1]?.mensaje || "")
    this.chats.forEach((x,index) => {x.ultimoMensaje = ultimoMensaje[index]})
  }
}
