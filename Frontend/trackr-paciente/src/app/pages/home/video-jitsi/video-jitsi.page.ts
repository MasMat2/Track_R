import { Component, OnInit, AfterViewInit  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule, NavController } from '@ionic/angular';
import { HeaderComponent } from '../layout/header/header.component';
import { ActivatedRoute, Router } from '@angular/router';

import { AudioInterface, ParticipantInterface } from './interfaces/jitsi-interface';
import { DataJitsiService } from './service-jitsi/data-jitsi.service';
import { CallJitsiComponent } from './call-jitsi/call-jitsi.component';
import { CreateJitsiMeetComponent } from './create-jitsi-meet/create-jitsi-meet.component';
import { ChatMensajeHubService } from 'src/app/services/dashboard/chat-mensaje-hub.service';
import { ChatHubServiceService } from 'src/app/services/dashboard/chat-hub-service.service';
import { AnswerMeetComponent } from './answer-meet/answer-meet.component';

declare var JitsiMeetExternalAPI: any;


@Component({
  selector: 'app-video-jitsi',
  templateUrl: './video-jitsi.page.html',
  styleUrls: ['./video-jitsi.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, HeaderComponent, CallJitsiComponent, CreateJitsiMeetComponent, AnswerMeetComponent]
})
export class VideoJitsiPage implements OnInit {

  protected localStream: MediaStream;

  protected idChat: string;
  protected meetName: string;

  protected room: string;
  protected user: string;
  protected jitsiDataUp: boolean = false;

  protected newMeet: boolean = false;


  constructor(private dataJitsiService: DataJitsiService,
    private ChatMensajeHubService:ChatMensajeHubService,
    private ChatHubServiceService:ChatHubServiceService,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit() { 
    this.ChatHubServiceService.iniciarConexion();
    this.ChatMensajeHubService.iniciarConexion();

    this.activatedRoute.paramMap.subscribe(params => {
      this.idChat = params.get('id-chat')!;
      this.meetName = params.get('meet-name')!;
    });
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
  };


}