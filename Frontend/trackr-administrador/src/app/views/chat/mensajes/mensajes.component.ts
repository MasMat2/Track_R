import { Component, Input } from '@angular/core';
import { ChatMensajeDTO } from '@dtos/chats/chat-mensaje-dto';
import { ChatMensajeHubService } from '../../../shared/services/chat-mensaje-hub.service';
import { ChatPersonaService } from '../../../shared/http/chats/chat-persona.service';
import { ChatPersonaSelectorDTO } from '@dtos/chats/chat-persona-selector-dto';

@Component({
  selector: 'app-mensajes',
  templateUrl: './mensajes.component.html',
  styleUrls: ['./mensajes.component.scss']
})
export class MensajesComponent {
  @Input() mensajes : ChatMensajeDTO[];
  @Input() idChat: number;
  @Input() tituloChat:string;
  protected msg: string;
  protected idUsuario:number;
  protected personas: ChatPersonaSelectorDTO[]

  constructor(private ChatMensajeHubService:ChatMensajeHubService,
              private ChatPersonaService:ChatPersonaService) {}
  
  ngOnInit(){
    this.obtenerIdUsuario();
    this.obtenerPersonasEnChat();
  }

  ngOnChanges(){
    this.obtenerPersonasEnChat();
  }

  obtenerPersonasEnChat(){
    this.ChatPersonaService.obtenerPersonasEnChatSelector(this.idChat).subscribe(res =>{
      this.personas = res;
    })
  }

  enviarMensaje(): void{
    let msg: ChatMensajeDTO = {
      fecha: new Date(),
      idChat: this.idChat,
      mensaje: this.msg,
      idPersona:5333
    }

    this.ChatMensajeHubService.enviarMensaje(msg);
    if(this.mensajes.length == 0){
      this.ChatMensajeHubService.chatMensaje$.subscribe(res => {
        this.mensajes = res.find(array => array.some(x => x.idChat === this.idChat)) || [];
      })
    }
    this.msg = "";

  }

  obtenerIdUsuario(){
    this.ChatPersonaService.obtenerIdUsuario().subscribe(res => {
      this.idUsuario = res;
    })
  }

  mostrarMensaje(id:number){
    return id == this.idUsuario;
  }
}
