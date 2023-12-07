import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ChatDTO } from '@dtos/chats/chat-dto';

@Component({
  selector: 'app-barra-chats',
  templateUrl: './barra-chats.component.html',
  styleUrls: ['./barra-chats.component.scss']
})
export class BarraChatsComponent {
  @Input() chats:ChatDTO[];
  @Output() idChatPadre = new EventEmitter<number>();

  enviarIdChat(idChat:number){
    this.idChatPadre.emit(idChat)
  }
}
