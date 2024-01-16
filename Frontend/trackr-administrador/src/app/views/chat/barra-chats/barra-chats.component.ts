import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ChatDTO } from '@dtos/chats/chat-dto';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CrearChatComponent } from '../crear-chat/crear-chat.component';

@Component({
  selector: 'app-barra-chats',
  templateUrl: './barra-chats.component.html',
  styleUrls: ['./barra-chats.component.scss']
})
export class BarraChatsComponent {
  @Input() chats:ChatDTO[];
  @Output() idChatPadre = new EventEmitter<number>();
  @Input() ultmoMensajes: string[];

  constructor(private modal:BsModalService){}

  enviarIdChat(idChat:number){
    this.idChatPadre.emit(idChat)
  }

  abrirModal(){
    this.modal.show(CrearChatComponent,{
      class: "modal-xl modal-dialog-centered"
    });
  }
}
