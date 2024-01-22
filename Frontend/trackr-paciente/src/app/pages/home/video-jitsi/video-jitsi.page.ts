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

declare var JitsiMeetExternalAPI: any;


@Component({
  selector: 'app-video-jitsi',
  templateUrl: './video-jitsi.page.html',
  styleUrls: ['./video-jitsi.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, HeaderComponent, CallJitsiComponent, CreateJitsiMeetComponent]
})
export class VideoJitsiPage implements OnInit {

  room: string;
  user: string;
  jitsiDataUp: boolean = false;

  newMeet: boolean = false;


  constructor(private dataJitsiService: DataJitsiService) { }

  ngOnInit() { }

  submitForm() {
    this.dataJitsiService.room = this.room;
    this.dataJitsiService.user = this.user;
    this.jitsiDataUp = true;
  }

  newMeetJitsi(){
    this.newMeet = true;
  }


}