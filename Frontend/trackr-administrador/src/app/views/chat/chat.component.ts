import { Component } from '@angular/core';
import { ChatDTO } from '@dtos/chats/chat-dto';
import { ChatHubServiceService } from '@services/chat-hub-service.service';
import { Observable } from 'rxjs';
import { ChatMensajeHubService } from '../../shared/services/chat-mensaje-hub.service';
import { ChatMensajeDTO } from '@dtos/chats/chat-mensaje-dto';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
  protected chats$: Observable<ChatDTO[]>;
  protected chats:ChatDTO[] = [];
  protected chatMensajes$: Observable<ChatMensajeDTO[][]>
  protected mensajes:ChatMensajeDTO[][] = [];
  protected contenido:string;
  protected idChatSeleccionado: number;
  protected mensajesChatSeleccionado: ChatMensajeDTO[]; 
  protected tituloChatSeleccionado: string;

  protected clickEnChat = false;

  constructor(
    private ChatHubServiceService:ChatHubServiceService,
    private chatMensajeHubService:ChatMensajeHubService,
    private route:ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.obtenerChats();
    //this.obtenerMensajes();
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
    this.chats$ = this.ChatHubServiceService.chat$
    this.chats$.subscribe(res => {
      this.chats = res;
      this.obtenerUltimoMensaje();
      this.obtenerMensajes();
    })
  }

  obtenerMensajes(){
    this.chatMensajes$ = this.chatMensajeHubService.chatMensaje$

    this.chatMensajes$.subscribe(res =>{
      this.mensajes = res;
      this.obtenerUltimoMensaje();
      this.obtenerId();
      // this.route.params.subscribe(params => {
      //   const idChat = Number(params['id']);
      //   if(idChat !== undefined){
      //     this.idChatSeleccionado = idChat;
      //     this.obtenerChatSeleccionado(idChat);
      //   }
      // })
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
    this.idChatSeleccionado = id;
    this.mensajesChatSeleccionado = this.mensajes.find(array => array.some(x => x.idChat === this.idChatSeleccionado)) || [];
    let aux = this.chats.find(x => x.idChat == id)
    this.tituloChatSeleccionado = aux?.titulo || "";
    this.clickEnChat = true;
  }

  obtenerUltimoMensaje():void{
    let ultimoMensaje = this.mensajes.map(arr => arr[arr.length - 1]?.mensaje || "")
    this.chats.forEach((x,index) => {x.ultimoMensaje = ultimoMensaje[index]})
  }
}
