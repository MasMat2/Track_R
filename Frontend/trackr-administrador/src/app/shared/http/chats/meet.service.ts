import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { SafeUrl } from '@angular/platform-browser';
import { ChatMensajeDTO } from '@dtos/chats/chat-mensaje-dto';
import { ChatPersonaService } from './chat-persona.service';
import { Notificacion } from '@dtos/notificaciones/notificacion-dto';

@Injectable({
  providedIn: 'root'
})
export class MeetService {
  constructor(private router: Router, private chatPersonaService : ChatPersonaService) {}


  public async redirigirMeet(notificacion : Notificacion){
    const regexTrackr = /trackr-\d+-\d+/;
    const regexWebrtc = /webrtc-\d+-(\d+)/;
    const mensaje = notificacion.mensaje;

    if (regexTrackr.test(mensaje)) {
      const match = mensaje.match(regexTrackr);
      if (match && match.length > 0) {
        const codigo = match[0];
        this.contestarLlamada(codigo, 'jitsi');
      } else {
        console.log('Error al validar codigo meet jitsi.');
      }
    } else if (regexWebrtc.test(mensaje)) {
      const match = mensaje.match(regexWebrtc);
      if (match && match.length > 0) {
        const codigo = match[1];
        this.contestarLlamada(codigo, 'webrtc');
      } else {
        console.log('Error al validar codigo meet webrtc.');
      }
    }else if(notificacion.idChat){
      await this.router.navigate(['administrador', 'chat' ]);
      this.chatPersonaService.emitirIdChatPadre(notificacion.idChat);
    }


  }

  private contestarLlamada(meetCode: string, tipo: 'jitsi' | 'webrtc') {
    if (tipo === 'jitsi') {
      this.router.navigate(['administrador', 'jitsi-meet', meetCode]);
    } else if (tipo === 'webrtc') {
      this.router.navigate(['/administrador/webrtc', meetCode]);
    }
  }
}