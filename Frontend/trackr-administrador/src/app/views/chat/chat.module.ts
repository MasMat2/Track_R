import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatComponent } from '../chat/chat.component';
import { FormsModule } from '@angular/forms';
import { BarraChatsComponent } from './barra-chats/barra-chats.component';
import { MensajesComponent } from './mensajes/mensajes.component';
import { CrearChatComponent } from './crear-chat/crear-chat.component';
import { TableModule } from 'primeng/table';
import { NgSelectModule } from '@ng-select/ng-select';
import { PipesModule } from 'src/app/shared/pipes/pipes.module';
import { ModalBaseComponent } from '@sharedComponents/modal-base/modal-base.component';
import { ModalBaseModule } from '@sharedComponents/modal-base/modal-base.module';
import { MatListModule } from '@angular/material/list'
import { MatIconModule } from '@angular/material/icon';
import { NgAudioRecorderModule } from 'ng-audio-recorder';



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
    NgSelectModule,
    PipesModule,
    ModalBaseModule,
    MatListModule,
    MatIconModule,
    NgAudioRecorderModule
  ]
})
export class ChatModule { }
