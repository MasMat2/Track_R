import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { HeaderComponent } from '../layout/header/header.component';

@Component({
  selector: 'app-cuestionarios',
  templateUrl: './cuestionarios.page.html',
  styleUrls: ['./cuestionarios.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    HeaderComponent,
  ]
})
export class CuestionariosPage implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
