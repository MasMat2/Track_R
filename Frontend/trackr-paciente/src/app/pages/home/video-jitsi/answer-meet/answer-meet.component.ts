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
  private appIDJitsi = 'vpaas-magic-cookie-c25fa0da2cd344ba8d41a873768065ec';


  // For Custom Controls
  isAudioMuted = false;
  isVideoMuted = false;

  constructor(
    private orientationService: ScreenOrientationService,
    private route: ActivatedRoute,
    private router: Router,
    private meta: Meta,
  ) { }

  ngOnInit() {

    this.route.paramMap.subscribe(params => {
      this.meetName = this.appIDJitsi+'/'+params.get('meet-name')!;
    });

    this.orientationService.lockLandscape();

    this.iniciarWebCam(); //iniciamos camara y microfono para que pueda ser iniciada una llamada en el iframe de jitsi

    this.answerMeet(); //Metodo para crear una llamada con jitsi
  }

  iniciarWebCam = async () => {
    this.localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
  };

  answerMeet(): void {

    //Configuración para la nueva sala
    const newRoomOptions = {
      roomName: this.meetName,
      width: 900,
      height: 500,
      configOverwrite: { prejoinPageEnabled: false },
      interfaceConfigOverwrite: {
        //overwrite interface properties
      },
      parentNode: document.querySelector('#jaas-container'),
      userInfo: {
        displayName: "Medico"
      }

    };

    //Crear una nueva instancia de JitsiMeetExternalAPI para la nueva sala
    this.api = new JitsiMeetExternalAPI(this.domain, newRoomOptions);

    // Event handlers para la nueva sala
    this.api.addEventListeners({
      readyToClose: this.handleClose,
      participantLeft: this.handleParticipantLeft,
      participantJoined: this.handleParticipantJoined,
      videoConferenceJoined: this.handleVideoConferenceJoined,
      videoConferenceLeft: this.handleVideoConferenceLeft,
      audioMuteStatusChanged: this.handleMuteStatus,
      videoMuteStatusChanged: this.handleVideoStatus
    });
  }

  executeCommand(command: string) {
    this.api.executeCommand(command);
    if (command == 'hangup') {
      this.router.navigate(['/home']);
      this.orientationService.lockPortrait();
      this.api.dispose();
      this.localStream.getTracks().forEach(track => track.stop());
      this.api.remove();

      return;
    }

    if (command == 'toggleAudio') {
      this.isAudioMuted = !this.isAudioMuted;
    }

    if (command == 'toggleVideo') {
      this.isVideoMuted = !this.isVideoMuted;
    }
  }

  handleClose = () => {
    console.log("handleClose");
  }

  handleParticipantLeft = async (participant: ParticipantInterface) => {
    console.log("handleParticipantLeft", participant); // { id: "2baa184e" }
  }

  handleParticipantJoined = async (participant: ParticipantInterface) => {
    console.log("handleParticipantJoined", participant); // { id: "2baa184e", displayName: "Shanu Verma", formattedDisplayName: "Shanu Verma" }
    const data = await this.getParticipants();
  }

  handleVideoConferenceJoined = async (participant: ParticipantInterface) => {
    console.log("handleVideoConferenceJoined", participant); // { roomName: "bwb-bfqi-vmh", id: "8c35a951", displayName: "Akash Verma", formattedDisplayName: "Akash Verma (me)"}
    const data = await this.getParticipants();
  }

  handleVideoConferenceLeft = () => {
    console.log("handleVideoConferenceLeft");
  }

  handleMuteStatus = (audio: AudioInterface) => {
    console.log("handleMuteStatus", audio); // { muted: true }
  }

  handleVideoStatus = (video: AudioInterface) => {
    console.log("handleVideoStatus", video); // { muted: true }
  }

  getParticipants() {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve(this.api.getParticipantsInfo()); // get all participants
      }, 500)
    });
  }

}
