import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';

@Component({
  selector: 'app-cuestionarios',
  templateUrl: './cuestionarios.page.html',
  styleUrls: ['./cuestionarios.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
  ]
})
export class CuestionariosPage implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
