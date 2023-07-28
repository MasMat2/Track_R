import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';

@Component({
  selector: 'app-widget-peso',
  templateUrl: './widget-peso.component.html',
  styleUrls: ['./widget-peso.component.scss'],
  standalone: true,
  imports: [
    WidgetComponent,
  ]
})
export class WidgetPesoComponent  implements OnInit {

  protected pesoPerdido: number = 12;
  protected pesoFaltante: number = 5;
  protected pesoActual: number = 85;
  protected dias: number = 40;

  constructor() { }

  ngOnInit() {}

}
