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

  protected pesoPerdido: number = 12;
  protected pesoFaltante: number = 5;
  protected pesoActual: number = 61;
  protected dias: number = 40;

  constructor() { }

  ngOnInit() {}

}
