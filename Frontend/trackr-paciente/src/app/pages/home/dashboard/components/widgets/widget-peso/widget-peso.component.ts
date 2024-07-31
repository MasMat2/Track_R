import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { WidgetComponent } from '../widget/widget.component';
import { addIcons } from 'ionicons';

@Component({
  selector: 'app-widget-peso',
  templateUrl: './widget-peso.component.html',
  styleUrls: ['./widget-peso.component.scss'],
  standalone: true,
  
  imports: [
    WidgetComponent,
    IonicModule,
  ]
})
export class WidgetPesoComponent  implements OnInit {

  protected peso: number = 0;
  protected unidadMedida: string = "kg";
  constructor() {
    addIcons({
      'peso': 'assets/img/svg/Peso.svg'
    })
   }

  ngOnInit() {}

}
