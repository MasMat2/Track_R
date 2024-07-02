import { Component, OnInit } from '@angular/core';
import { Header } from 'primeng/api';
import { HeaderComponent } from '../layout/header/header.component';
import { IonicModule } from '@ionic/angular';
import { Observable } from 'rxjs';
import { ChatDTO } from 'src/app/shared/Dtos/Chat/chat-dto';
import { ChatMensajeDTO } from 'src/app/shared/Dtos/Chat/chat-mensaje-dto';
import { ChatMensajeHubService } from 'src/app/services/dashboard/chat-mensaje-hub.service';
import { ChatHubServiceService } from 'src/app/services/dashboard/chat-hub-service.service';
import { BarraChatsComponent } from './BarraChats/BarraChats.component';
import { MensajesComponent } from './mensajes/mensajes.component';
import { CommonModule } from '@angular/common';
import { DomSanitizer } from '@angular/platform-browser';
import { ArchivoService } from '@services/archivo.service';

@Component({
  selector: 'app-chat-movil',
  templateUrl: './chat-movil.page.html',
  styleUrls: ['./chat-movil.page.scss'],
  standalone: true,
  imports: [
    HeaderComponent,
    IonicModule,
    BarraChatsComponent,
    MensajesComponent,
    CommonModule,
  ],
})
export class ChatMovilComponent implements OnInit {
  protected chats$: Observable<ChatDTO[]>;
  protected chats: ChatDTO[] = [];
  protected chatMensajes$: Observable<ChatMensajeDTO[][]>;
  protected mensajes: ChatMensajeDTO[][] = [];
  protected contenido: string;
  protected idChatSeleccionado: number;
  protected mensajesChatSeleccionado: ChatMensajeDTO[];

  protected clickEnChat = false;

  constructor(
    private ChatHubServiceService: ChatHubServiceService,
    private chatMensajeHubService: ChatMensajeHubService,
    private archivoService : ArchivoService,
    private sanitizer : DomSanitizer
  ) {}

  ngOnInit() {
    this.obtenerChats();
    this.obtenerMensajes();
  }

  obtenerChats() {
    this.chats$ = this.ChatHubServiceService.chat$;
    this.chats$.subscribe((chats) => {
      this.chats = chats;
      this.obtenerUltimoMensaje();
    });
  }

  obtenerMensajes() {
    this.chatMensajes$ = this.chatMensajeHubService.chatMensaje$;

    this.chatMensajes$.subscribe((res) => {
      this.mensajes = res;
      this.obtenerUltimoMensaje();
    });
  }

  enviarMensaje(idChat: number): void {
    let msg: ChatMensajeDTO = {
      fecha: new Date(),
      idChat,
      mensaje: this.contenido,
      idPersona: 5333,
      idArchivo: 0,
    };

    this.chatMensajeHubService.enviarMensaje(msg);
  }

  obtenerChatSeleccionado(id: number) {
    this.idChatSeleccionado = id;
    this.mensajesChatSeleccionado =
      this.mensajes.find((array) =>
        array.some((x) => x.idChat === this.idChatSeleccionado)
      ) || [];

    this.clickEnChat = true;
  }

  obtenerUltimoMensaje(): void {
    let ultimoMensaje = this.mensajes.map(
      (arr) =>{ return {mensajes: arr[arr.length - 1]?.mensaje || '', chat: arr[0]?.idChat || 0}}
    );

    let fechaUltimoMensaje = this.mensajes.map(
      (arr) => {
        return {fecha: arr[arr.length - 1]?.fecha || new Date(), chat: arr[0]?.idChat || 0}
      }
    )
    
    this.chats.forEach((x) => {
      x.ultimoMensaje = ultimoMensaje.filter(y => y.chat == x.idChat)[0]?.mensajes || '';
      x.fechaUltimoMensaje = fechaUltimoMensaje.filter(y => y.chat == x.idChat)[0]?.fecha || new Date();
    });
  }
}
