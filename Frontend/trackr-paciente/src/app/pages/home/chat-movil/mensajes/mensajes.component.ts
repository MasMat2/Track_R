import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ChatPersonaService } from '@http/chat/chat-persona.service';
import { ChatMensajeHubService } from 'src/app/services/dashboard/chat-mensaje-hub.service';
import { ChatMensajeDTO } from 'src/app/shared/Dtos/Chat/chat-mensaje-dto';

@Component({
  selector: 'app-mensajes',
  templateUrl: './mensajes.component.html',
  styleUrls: ['./mensajes.component.scss'],
  standalone: true,
  imports: [FormsModule, CommonModule],
})
export class MensajesComponent {
  @Input() mensajes: ChatMensajeDTO[];
  @Input() idChat: number;
  protected msg: string;
  protected idUsuario: number;

  constructor(
    private ChatMensajeHubService: ChatMensajeHubService,
    private ChatPersonaService: ChatPersonaService
  ) {}

  ngOnInit() {
    this.obtenerIdUsuario();
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
}
