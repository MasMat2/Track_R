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
import { LucideAngularModule, Plus, Trash2 } from 'lucide-angular';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatRippleModule } from '@angular/material/core';
import { AudioWaveComponent } from '@sharedComponents/audio-wave/audio-wave.component';
import {TextFieldModule} from '@angular/cdk/text-field';
import { MatFormFieldModule } from '@angular/material/form-field';



@NgModule({
  declarations: [
    ChatComponent,
    BarraChatsComponent,
    MensajesComponent,
    CrearChatComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    TableModule,
    NgSelectModule,
    PipesModule,
    ModalBaseModule,
    MatListModule,
    MatIconModule,
    NgAudioRecorderModule,
    MatButtonModule,
    MatRippleModule,
    LucideAngularModule.pick({Plus, Trash2}),
    AudioWaveComponent,
    MatFormFieldModule,
    TextFieldModule
  ],
  exports: [BarraChatsComponent]
})
export class ChatModule { }
