import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { HeaderComponent } from '../layout/header/header.component';

@Component({
  selector: 'app-muestras',
  templateUrl: './muestras.page.html',
  styleUrls: ['./muestras.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    HeaderComponent
  ]
})
export class MuestrasPage implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
