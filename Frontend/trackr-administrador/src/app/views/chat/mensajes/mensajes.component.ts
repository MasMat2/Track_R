import { Component, Input } from '@angular/core';
import { ChatMensajeDTO } from '@dtos/chats/chat-mensaje-dto';
import { ChatMensajeHubService } from '../../../shared/services/chat-mensaje-hub.service';
import { ChatPersonaService } from '../../../shared/http/chats/chat-persona.service';

@Component({
  selector: 'app-mensajes',
  templateUrl: './mensajes.component.html',
  styleUrls: ['./mensajes.component.scss']
})
export class MensajesComponent {
  @Input() mensajes : ChatMensajeDTO[];
  @Input() idChat: number;
  protected msg: string;
  protected idUsuario:number;

  constructor(private ChatMensajeHubService:ChatMensajeHubService,
              private ChatPersonaService:ChatPersonaService) {}
  
  ngOnInit(){
    this.obtenerIdUsuario();
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
