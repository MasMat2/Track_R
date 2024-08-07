import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { BarraChatsComponent } from './BarraChats/BarraChats.component';
import { MensajesComponent } from './mensajes/mensajes.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-chat-movil',
  templateUrl: './chat-movil.page.html',
  styleUrls: ['./chat-movil.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    BarraChatsComponent,
    MensajesComponent,
    CommonModule,
  ],
})
export class ChatMovilComponent implements OnInit {

  constructor(

  ) {}

  ngOnInit() {

  }

}
