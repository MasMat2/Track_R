import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { barChartOutline, scaleOutline } from 'ionicons/icons';

@Component({
  selector: 'app-widget-omron',
  templateUrl: './widget-omron.component.html',
  styleUrls: ['./widget-omron.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    WidgetComponent,
    IonicModule
  ]
})
export class WidgetOmronComponent  implements OnInit {

  constructor() {
    addIcons({barChartOutline})
   }

  protected suenoActual: number = 0;
  protected horasMinDiarias: number = 0;
  protected minutosMinDiarias: number = 0;
  protected tiempoDormido: number = 0;
  protected minutostiempoDormido: number = 0;
  protected suenoProfundo: number = 0;
  protected minsuenoProfundo: number = 0;
  ngOnInit() {}

   openOmronConnect() {
    console.log('openOmronConnect');
   }

}
