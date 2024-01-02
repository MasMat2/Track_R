import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';
import {home, videocam, statsChart, documentText, personCircle, chatboxEllipses} from 'ionicons/icons'



@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
  ]
})
export class FooterComponent implements OnInit {
  constructor() {
    addIcons({home, videocam, statsChart, documentText, personCircle, chatboxEllipses})
  }

  public ngOnInit(): void {}
}
