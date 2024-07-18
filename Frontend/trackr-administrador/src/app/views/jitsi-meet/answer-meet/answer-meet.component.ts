import { Component, OnInit } from '@angular/core';
import { Meta } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { AudioInterface, ParticipantInterface } from '../interfaces/jitsi-interface';

declare var JitsiMeetExternalAPI: any;

@Component({
  selector: 'app-answer-meet',
  templateUrl: './answer-meet.component.html',
  styleUrls: ['./answer-meet.component.scss']
})
export class AnswerMeetComponent implements OnInit {

  protected localStream: MediaStream;

  protected domain: string = "8x8.vc"; // For self hosted use your domain
  protected room: any;
  protected options: any;
  protected newRoomApi: any;
  protected user: any;
  private AppIDJitsi = 'vpaas-magic-cookie-c25fa0da2cd344ba8d41a873768065ec';

  private meetName: string;

  // For Custom Controls
  isAudioMuted = false;
  isVideoMuted = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private meta: Meta
  ) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.meetName = this.AppIDJitsi + '/' + params.get('meet-name')!;
    });

    this.iniciarWebCam();
    const cspValue = "default-src 'self' data: gap: https://ssl.gstatic.com 'unsafe-eval'; style-src 'self' 'unsafe-inline'; media-src *; img-src 'self' data: content:;";
    this.meta.addTag({ name: 'Content-Security-Policy', content: cspValue }); //Se le indica al template que confie en iframe


    this.answerMeet(); //Metodo para crear una llamada con jitsi
  }

  answerMeet(): void {

    //Configuración para la nueva sala
    const newRoomOptions = {
      roomName: this.meetName,
      width: 1400,
      height: 720,
      configOverwrite: { prejoinPageEnabled: false },
      interfaceConfigOverwrite: {
        //overwrite interface properties
      },
      parentNode: document.querySelector('#jaas-container'),
      userInfo: {
        displayName: "Doctor"
      }

    };

    //Crear una nueva instancia de JitsiMeetExternalAPI para la nueva sala
    const newRoomApi = new JitsiMeetExternalAPI(this.domain, newRoomOptions);

    // Event handlers para la nueva sala
    newRoomApi.addEventListeners({
      readyToClose: this.handleClose,
      participantLeft: this.handleParticipantLeft,
      participantJoined: this.handleParticipantJoined,
      videoConferenceJoined: this.handleVideoConferenceJoined,
      videoConferenceLeft: this.handleVideoConferenceLeft,
      audioMuteStatusChanged: this.handleMuteStatus,
      videoMuteStatusChanged: this.handleVideoStatus
    });

  }

  iniciarWebCam = async () => {
    this.localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
  };

  handleClose = () => {
    this.newRoomApi.dispose();
  }

  handleParticipantLeft = async (participant: ParticipantInterface) => {
    //console.log("handleParticipantLeft", participant);  { id: "2baa184e" }
    const data = await this.getParticipants();
  }

  handleParticipantJoined = async (participant: ParticipantInterface) => {
    //console.log("handleParticipantJoined", participant);  { id: "2baa184e", displayName: "Shanu Verma", formattedDisplayName: "Shanu Verma" }
    const data = await this.getParticipants();
  }

  handleVideoConferenceJoined = async (participant: ParticipantInterface) => {
    //console.log("handleVideoConferenceJoined", participant);  { roomName: "bwb-bfqi-vmh", id: "8c35a951", displayName: "Akash Verma", formattedDisplayName: "Akash Verma (me)"}
    const data = await this.getParticipants();
  }

  handleVideoConferenceLeft = () => {
    this.newRoomApi.dispose();
    this.router.navigate(['/thank-you']);
  }

  handleMuteStatus = (audio: AudioInterface) => {
    //console.log("handleMuteStatus", audio);  { muted: true }
  }

  handleVideoStatus = (video: AudioInterface) => {
    //console.log("handleVideoStatus", video);  { muted: true }
  }

  getParticipants() {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve(this.newRoomApi.getParticipantsInfo()); // get all participants
      }, 500)
    });
  }

  executeCommand(command: string) {
    this.newRoomApi.executeCommand(command);;
    if (command == 'hangup') {
      this.localStream.getTracks().forEach((track) => track.stop());
      this.newRoomApi.dispose();
      this.newRoomApi.remove();
      this.router.navigate(['/administrador/chat']);
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
