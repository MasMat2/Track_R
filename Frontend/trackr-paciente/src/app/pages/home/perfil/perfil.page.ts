import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { InformacionGeneralComponent } from './informacion-general/components/info-general/informacion-general.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.page.html',
  styleUrls: ['./perfil.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    RouterModule,
    InformacionGeneralComponent,
  ]
})
export class PerfilPage implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
