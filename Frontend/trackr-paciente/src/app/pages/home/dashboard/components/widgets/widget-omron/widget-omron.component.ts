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
    addIcons({
      'bar-chart': 'assets/img/svg/bar-chart-4.svg'
    })
   }

  ngOnInit() {}

   openOmronConnect() {

   }

}
