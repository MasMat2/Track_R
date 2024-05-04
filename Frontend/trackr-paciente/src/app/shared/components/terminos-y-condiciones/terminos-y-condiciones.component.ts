import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { ModalController } from '@ionic/angular/standalone';
import { chevronBack } from 'ionicons/icons';
import { addIcons } from 'ionicons';

@Component({
  selector: 'app-terminos-y-condiciones',
  templateUrl: './terminos-y-condiciones.component.html',
  styleUrls: ['./terminos-y-condiciones.component.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, RouterModule]
})
export class TerminosYCondicionesComponent  implements OnInit {

  constructor(private modalCtrl: ModalController) { addIcons({chevronBack})}

  ngOnInit() {}

  cerrarModal() {
    return this.modalCtrl.dismiss(null, 'cancel');
  }

}
