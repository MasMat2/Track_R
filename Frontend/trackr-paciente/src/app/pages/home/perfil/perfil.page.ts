import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { InformacionGeneralComponent } from './informacion-general/informacion-general.component';
import { HeaderComponent } from '../layout/header/header.component';
import { MisDoctoresPage } from './mis-doctores/mis-doctores.page';
import { MisTratamientosComponent } from './mis-tratamientos/mis-tratamientos.component';
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
    HeaderComponent,
    MisDoctoresPage,
    RouterModule,
    MisTratamientosComponent
  ]
})
export class PerfilPage implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
