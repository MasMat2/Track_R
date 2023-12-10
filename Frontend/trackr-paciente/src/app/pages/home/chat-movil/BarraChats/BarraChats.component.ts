import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ChatDTO } from 'src/app/shared/Dtos/Chat/chat-dto';

@Component({
  selector: 'app-barra-chats',
  templateUrl: './BarraChats.component.html',
  styleUrls: ['./BarraChats.component.scss'],
  standalone: true,
  imports: [TableModule, CommonModule],
})
export class BarraChatsComponent {
  @Input() chats: ChatDTO[];
  @Output() idChatPadre = new EventEmitter<number>();
  @Input() ultmoMensajes: string[];

  constructor() {}

  enviarIdChat(idChat: number) {
    this.idChatPadre.emit(idChat);
  }
}
