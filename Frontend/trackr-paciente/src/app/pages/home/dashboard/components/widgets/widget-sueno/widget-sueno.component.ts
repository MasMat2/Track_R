import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';

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

  constructor() {
    addIcons({
      'suenio': '/assets/img/svg/Suenio.svg'
    })
   }

   protected horas: number = 0;
   protected minutos: number = 0;
   
  ngOnInit() {}

}
