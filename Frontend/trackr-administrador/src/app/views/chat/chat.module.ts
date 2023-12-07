import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatComponent } from '../chat/chat.component';
import { FormsModule } from '@angular/forms';
import { BarraChatsComponent } from './barra-chats/barra-chats.component';
import { MensajesComponent } from './mensajes/mensajes.component';
import { CrearChatComponent } from './crear-chat/crear-chat.component';
import { TableModule } from 'primeng/table';
import { NgSelectModule } from '@ng-select/ng-select';



@NgModule({
  declarations: [
    ChatComponent,
    BarraChatsComponent,
    MensajesComponent,
    CrearChatComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    TableModule,
    NgSelectModule
  ]
})
export class ChatModule { }
