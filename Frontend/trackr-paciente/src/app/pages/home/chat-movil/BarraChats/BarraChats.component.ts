import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { TableModule } from 'primeng/table';
import { Observable } from 'rxjs';
import { ChatDTO } from 'src/app/shared/Dtos/Chat/chat-dto';
import { ChatHubServiceService } from '../../../../services/dashboard/chat-hub-service.service';
import { IonicModule } from '@ionic/angular';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { ChatMensajeHubService } from '../../../../services/dashboard/chat-mensaje-hub.service';
import { ChatMensajeDTO } from 'src/app/shared/Dtos/Chat/chat-mensaje-dto';

@Component({
  selector: 'app-barra-chats',
  templateUrl: './BarraChats.component.html',
  styleUrls: ['./BarraChats.component.scss'],
  standalone: true,
  imports: [TableModule, CommonModule,IonicModule,HeaderComponent],
})
export class BarraChatsComponent {
  protected chats$: Observable<ChatDTO[]>;
  protected chats: ChatDTO[];
  protected chatMensajes$: Observable<ChatMensajeDTO[][]>;
  protected mensajes: ChatMensajeDTO[][];
  //@Output() idChatPadre = new EventEmitter<number>();
  //@Input() ultmoMensajes: string[];

  constructor(private router: Router,
              private ChatHubServiceService:ChatHubServiceService,
              private chatMensajeHubService:ChatMensajeHubService) {}

  ionViewWillEnter(){
    this.obtenerChats()
  }

  obtenerChats() {
    this.chats$ = this.ChatHubServiceService.chat$;
    this.chats$.subscribe((res) => {
      console.log(res);
      this.chats = res;
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

  obtenerUltimoMensaje(): void {
    let ultimoMensaje = this.mensajes.map(
      (arr) => arr[arr.length - 1]?.mensaje || ''
    );
    this.chats.forEach((x, index) => {
      x.ultimoMensaje = ultimoMensaje[index];
    });
  }

  enviarIdChat(idChat: number) {
    this.router.navigate(['home/chat-movil/chat',idChat]);
  }
}
