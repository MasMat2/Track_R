import { Component } from '@angular/core';
import { ChatDTO } from '@dtos/chats/chat-dto';
import { ChatHubServiceService } from '@services/chat-hub-service.service';
import { Observable } from 'rxjs';
import { ChatMensajeHubService } from '../../shared/services/chat-mensaje-hub.service';
import { ChatMensajeDTO } from '@dtos/chats/chat-mensaje-dto';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
  protected chats$: Observable<ChatDTO[]>;
  protected chatMensajes$: Observable<ChatMensajeDTO[]>
  protected mensajes:ChatMensajeDTO[] = [];
  protected contenido:string;

  constructor(
    private ChatHubServiceService:ChatHubServiceService,
    private chatMensajeHubService:ChatMensajeHubService
  ) { }

  ngOnInit(): void {
    this.chatMensajes$ = this.chatMensajeHubService.chatMensaje$
    console.log(this.chatMensajes$)

    this.chatMensajes$.subscribe(res =>{
      this.mensajes = res;
    })
  }

  enviarMensaje(): void{
    let msg: ChatMensajeDTO = {
      fecha: new Date(),
      idChat: 1,
      mensaje: this.contenido,
      idPersona:5334
    }

    this.chatMensajeHubService.enviarMensaje(msg);
  }
}
