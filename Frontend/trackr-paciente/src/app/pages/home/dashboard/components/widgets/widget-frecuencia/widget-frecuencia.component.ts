import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';

@Component({
  selector: 'app-widget-frecuencia',
  templateUrl: './widget-frecuencia.component.html',
  styleUrls: ['./widget-frecuencia.component.scss'],
  standalone: true,
  imports: [
    WidgetComponent,
    IonicModule
  ]
})
export class WidgetFrecuenciaComponent  implements OnInit {


  protected ritmoCardiaco: number = 0;
  protected unidadMedida: string = "lpm";

  constructor() {
    addIcons({
      'ritmo-cardiaco': 'assets/img/svg/Ritmo-cardiaco.svg'
    })
   }

  ngOnInit() {}

}
