import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { WidgetComponent } from '../widget/widget.component';

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

  protected pesoPerdido: number = 0;
  protected pesoFaltante: number = 0;
  protected pesoActual: number = 0;
  protected dias: number = 0;

  constructor() { }

  ngOnInit() {}

}
