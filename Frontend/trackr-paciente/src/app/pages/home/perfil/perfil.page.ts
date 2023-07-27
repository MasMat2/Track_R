import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { InformacionGeneralComponent } from './informacion-general/informacion-general.component';
import { HeaderComponent } from '../layout/header/header.component';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.page.html',
  styleUrls: ['./perfil.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    InformacionGeneralComponent,
    HeaderComponent
  ]
})
export class PerfilPage implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
