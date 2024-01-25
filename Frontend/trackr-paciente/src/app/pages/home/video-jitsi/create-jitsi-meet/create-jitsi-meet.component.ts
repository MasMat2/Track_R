import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';


import { AudioInterface, ParticipantInterface } from '../interfaces/jitsi-interface';
import { IonicModule, NavController } from '@ionic/angular';
import { DataJitsiService } from '../service-jitsi/data-jitsi.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { Meta } from '@angular/platform-browser';

import { ScreenOrientation, OrientationType } from '@capawesome/capacitor-screen-orientation';


declare var JitsiMeetExternalAPI: any;

@Component({
  selector: 'app-create-jitsi-meet',
  templateUrl: './create-jitsi-meet.component.html',
  styleUrls: ['./create-jitsi-meet.component.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule]
})
export class CreateJitsiMeetComponent implements OnInit {

  protected localStream: MediaStream;

  domain: string = "meet.jit.si"; // For self hosted use your domain
  room: any;
  options: any;
  api: any;
  user: any;

  // For Custom Controls
  isAudioMuted = false;
  isVideoMuted = false;

  constructor(
    private router: Router,
    private dataJitsiService: DataJitsiService,
    private meta: Meta,
  ) { }


  ngOnInit() {
    this.bloquearOrientacion(); //Para mejorar la visibilidad del iframe de jitsi
    this.iniciarWebCam(); //iniciamos camara y microfono para que pueda ser iniciada una llamada en el iframe de jitsi
    const cspValue = "default-src 'self' data: gap: https://ssl.gstatic.com 'unsafe-eval'; style-src 'self' 'unsafe-inline'; media-src *; img-src 'self' data: content:;";
    this.meta.addTag({ name: 'Content-Security-Policy', content: cspValue }); //Se le indica al template que confie en iframe

    this.createNewRoom(); //Metodo para crear una llamada con jitsi
  }


  async bloquearOrientacion() {
    try {
      await ScreenOrientation.lock({ type: OrientationType.LANDSCAPE });
    } catch (error) {
      console.error('Error Screen Orientation:', error);
    }
  }

  iniciarWebCam = async () => {
    this.localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
  };

  createNewRoom(): void {
    
    const newRoomName = 'meet-trackr-1212' //+ Math.floor(Math.random() * 1000);

    //Crea la nueva URL utilizando la plantilla de cadenas
    const newUrl = `intent://${this.domain}/${newRoomName}#Intent;scheme=org.jitsi.meet;package=org.jitsi.meet;end`;

    window.location.assign(newUrl);
    
    //Configuración para la nueva sala
    const newRoomOptions = {
      roomName: newRoomName,
      width: 900,
      height: 500,
      configOverwrite: { prejoinPageEnabled: false },
      interfaceConfigOverwrite: {
        //overwrite interface properties
      },
      parentNode: document.querySelector('#jitsi-new-meet-iframe'),
      userInfo: {
        displayName: "Trackr-user"
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

    console.log('Nueva sala creada:', newRoomName);

    //Redirigir a la nueva sala si es necesario
    // this.router.navigate(['/ruta-de-la-nueva-sala', newRoomName]);
  }

  handleClose = () => {
    console.log("handleClose");
  }

  handleParticipantLeft = async (participant: ParticipantInterface) => {
    console.log("handleParticipantLeft", participant); // { id: "2baa184e" }
    const data = await this.getParticipants();
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
    this.router.navigate(['/thank-you']);
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

  executeCommand(command: string) {
    this.api.executeCommand(command);;
    if (command == 'hangup') {
      this.router.navigate(['/thank-you']);
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
