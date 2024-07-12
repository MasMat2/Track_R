import { Component, OnInit, AfterViewInit, Input, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { IonicModule  } from '@ionic/angular';
import { DataJitsiService } from '../service-jitsi/data-jitsi.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


import { ScreenOrientationService } from '@services/screen-orientation.service';
import { ChatMensajeDTO } from 'src/app/shared/Dtos/Chat/chat-mensaje-dto';
import { Observable } from 'rxjs';
import { ChatDTO } from 'src/app/shared/Dtos/Chat/chat-dto';

declare var JitsiMeetExternalAPI: any;

@Component({
  selector: 'app-create-jitsi-meet',
  templateUrl: './create-jitsi-meet.component.html',
  styleUrls: ['./create-jitsi-meet.component.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule]
})
export class CreateJitsiMeetComponent implements OnInit, AfterViewInit {

  protected localStream: MediaStream;

  protected mensajes$: Observable<ChatMensajeDTO[][]>;
  protected mensajes : ChatMensajeDTO[][];
  protected chats$: Observable<ChatDTO[]>;
  protected chats: ChatDTO[] = [];

  // For Custom Controls
  private isAudioMuted = false;
  private isVideoMuted = false;

  @ViewChild('jaasContainer', { static: false }) jaasContainer: ElementRef;

  constructor(private dataJitsiService: DataJitsiService,
    private orientationService: ScreenOrientationService,
    private router: Router,) {}

  ngOnInit() {
    // Inicialización del componente
  }

  ngAfterViewInit() {

  }

  executeCommand(command: string) {

    //Terminar llamada
    if (command == 'hangup') {
      //Eliminar componente en el DOM
      const container = document.getElementById('jaas-container');
      if (container) {
        container.innerHTML = '';
      }
      this.router.navigate(['/home']);
      this.orientationService.lockPortrait();
      this.dataJitsiService.hangup();
      return;
    }

    if (command == 'toggleAudio') {
      this.isAudioMuted = !this.isAudioMuted;
    }

    if (command == 'toggleVideo') {
      this.isVideoMuted = !this.isVideoMuted;
    }
  }

}
