import { Component, OnInit, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule, NavController } from '@ionic/angular';
import { HeaderComponent } from '../layout/header/header.component';
import { Router } from '@angular/router';

import { AudioInterface, ParticipantInterface } from './interfaces/jitsi-interface';
import { DataJitsiService } from './service-jitsi/data-jitsi.service';
import { CallJitsiComponent } from './call-jitsi/call-jitsi.component';
import { CreateJitsiMeetComponent } from './create-jitsi-meet/create-jitsi-meet.component';
import { ChatMensajeHubService } from 'src/app/services/dashboard/chat-mensaje-hub.service';
import { ChatHubServiceService } from 'src/app/services/dashboard/chat-hub-service.service';

declare var JitsiMeetExternalAPI: any;


@Component({
  selector: 'app-video-jitsi',
  templateUrl: './video-jitsi.page.html',
  styleUrls: ['./video-jitsi.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, HeaderComponent, CallJitsiComponent, CreateJitsiMeetComponent]
})
export class VideoJitsiPage implements OnInit {

  protected localStream: MediaStream;

  room: string;
  user: string;
  jitsiDataUp: boolean = false;

  newMeet: boolean = false;


  constructor(private dataJitsiService: DataJitsiService,
    private ChatMensajeHubService:ChatMensajeHubService,
    private ChatHubServiceService:ChatHubServiceService) { }

  ngOnInit() { 
    this.ChatHubServiceService.iniciarConexion();
    this.ChatMensajeHubService.iniciarConexion();
  }

  submitForm() {
    this.dataJitsiService.room = this.room;
    this.dataJitsiService.user = this.user;
    this.jitsiDataUp = true;
  }

  newMeetJitsi(){
    this.newMeet = true;
  }

  webcamButton = async () => {
    this.localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
    /*this.remoteStream = new MediaStream();

    // Push tracks from local stream to peer connection
    this.localStream.getTracks().forEach((track) => {
      if (this.localStream) this.pc.addTrack(track, this.localStream);
    });

    // Pull tracks from remote stream, add to video stream
    this.pc.ontrack = (event) => {
      event.streams[0].getTracks().forEach((track) => {
        this.remoteStream.addTrack(track);
      });
    };

    this.buttonState = false;*/

  };


}