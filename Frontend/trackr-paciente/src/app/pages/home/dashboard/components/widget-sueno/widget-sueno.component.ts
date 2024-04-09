import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';

@Component({
  selector: 'app-widget-sueno',
  templateUrl: './widget-sueno.component.html',
  styleUrls: ['./widget-sueno.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    WidgetComponent,
    IonicModule
  ]
})
export class WidgetSuenoComponent  implements OnInit {

  constructor() { }

  protected suenoActual: number = 0;
  protected horasMinDiarias: number = 0;
  protected minutosMinDiarias: number = 0;
  protected tiempoDormido: number = 0;
  protected minutostiempoDormido: number = 0;
  protected suenoProfundo: number = 0;
  protected minsuenoProfundo: number = 0;
  ngOnInit() {}

}
