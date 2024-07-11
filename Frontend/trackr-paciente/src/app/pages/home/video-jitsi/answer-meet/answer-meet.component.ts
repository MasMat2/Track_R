import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { DataJitsiService } from '../service-jitsi/data-jitsi.service';
import { ScreenOrientationService } from '@services/screen-orientation.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AudioInterface, ParticipantInterface } from '../interfaces/jitsi-interface';
import { Meta } from '@angular/platform-browser';


declare var JitsiMeetExternalAPI: any;

@Component({
  selector: 'app-answer-meet',
  templateUrl: './answer-meet.component.html',
  styleUrls: ['./answer-meet.component.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule]
})
export class AnswerMeetComponent implements OnInit {
  protected localStream: MediaStream;

  protected domain: string = "8x8.vc"; // For self hosted use your domain
  protected room: any;
  protected options: any;
  protected api: any;
  protected user: any;
  private meetName: string;

  // For Custom Controls
  isAudioMuted = false;
  isVideoMuted = false;

  constructor(
    private orientationService: ScreenOrientationService,
    private router: Router,
    private dataJitsiService: DataJitsiService
  ) { }

  ngOnInit() {

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
