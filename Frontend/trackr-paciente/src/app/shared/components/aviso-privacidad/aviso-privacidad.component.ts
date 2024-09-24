
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ModalController } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { IonHeader } from "@ionic/angular/standalone";

import { IonicModule } from '@ionic/angular';

@Component({
  selector: 'app-aviso-privacidad',
  templateUrl: './aviso-privacidad.component.html',
  styleUrls: ['./aviso-privacidad.component.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, RouterModule]
})
export class AvisoPrivacidadComponent  implements OnInit {

  constructor(private modalCtrl: ModalController) { 
    addIcons({
      'chevron-left': 'assets/img/svg/chevron-left.svg'
    })
  }

  ngOnInit() {}

  cerrarModal() {
    return this.modalCtrl.dismiss(null, 'cancel');
  }
}
