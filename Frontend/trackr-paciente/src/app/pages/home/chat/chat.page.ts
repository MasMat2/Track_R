import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { HeaderComponent } from '../layout/header/header.component';
import { VideoChatPage } from './video-chat/video-chat.page';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.page.html',
  styleUrls: ['./chat.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    HeaderComponent,
    VideoChatPage
  ]
})
export class ChatPage implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
