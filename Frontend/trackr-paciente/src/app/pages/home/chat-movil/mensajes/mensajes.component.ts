import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ChatPersonaService } from '@http/chat/chat-persona.service';
import { IonicModule } from '@ionic/angular';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { Observable } from 'rxjs';
import { ChatMensajeHubService } from 'src/app/services/dashboard/chat-mensaje-hub.service';
import { ChatMensajeDTO } from 'src/app/shared/Dtos/Chat/chat-mensaje-dto';

@Component({
  selector: 'app-mensajes',
  templateUrl: './mensajes.component.html',
  styleUrls: ['./mensajes.component.scss'],
  standalone: true,
  imports: [FormsModule, CommonModule, IonicModule,HeaderComponent],
})
export class MensajesComponent {
  protected mensajes: ChatMensajeDTO[];
  protected idChat: number;
  protected msg: string;
  protected idUsuario: number;
  protected chatMensajes$: Observable<ChatMensajeDTO[][]>
  protected chatMensajes: ChatMensajeDTO[][]

  constructor(
    private ChatMensajeHubService: ChatMensajeHubService,
    private ChatPersonaService: ChatPersonaService,
    private router: ActivatedRoute,
    private route: Router
  ) {}

  ionViewWillEnter() {
    this.obtenerIdUsuario();
    this.obtenerIdChat();
  }

  obtenerIdChat(){
    this.router.params.subscribe(params => {
      this.idChat = params['id']
      this.obtenerMensajes();
      console.log(this.idChat)
    })
  }

  enviarMensaje(): void {
    let msg: ChatMensajeDTO = {
      fecha: new Date(),
      idChat: this.idChat,
      mensaje: this.msg,
      idPersona: 5333,
    };

    this.ChatMensajeHubService.enviarMensaje(msg);
    this.msg = '';
  }

  obtenerIdUsuario() {
    this.ChatPersonaService.obtenerIdUsuario().subscribe((res) => {
      this.idUsuario = res;
    });
  }

  mostrarMensaje(id: number) {
    return id == this.idUsuario;
  }

  obtenerMensajes() {
    this.chatMensajes$ = this.ChatMensajeHubService.chatMensaje$;

    this.chatMensajes$.subscribe((res) => {
      this.chatMensajes = res;
      this.obtenerChatSeleccionado();
    });
  }

  obtenerChatSeleccionado() {
      this.mensajes = this.chatMensajes.find((array) =>
        array.some((x) => x.idChat == this.idChat)
      ) || [];
  }

  regresarBtn(){
    this.route.navigate(['home/chat-movil'])
  }
}
