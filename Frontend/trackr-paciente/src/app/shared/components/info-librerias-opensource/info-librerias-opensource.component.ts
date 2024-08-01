import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { ModalController } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';

@Component({
  selector: 'app-terminos-y-condiciones',
  templateUrl: './info-librerias-opensource.component.html',
  styleUrls: ['./info-librerias-opensource.component.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, RouterModule]
})
export class InfoLibreriasOpenSourceComponent  implements OnInit {

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
