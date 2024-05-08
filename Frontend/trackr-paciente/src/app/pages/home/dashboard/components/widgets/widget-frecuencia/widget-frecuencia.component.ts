import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';

@Component({
  selector: 'app-widget-frecuencia',
  templateUrl: './widget-frecuencia.component.html',
  styleUrls: ['./widget-frecuencia.component.scss'],
  standalone: true,
  imports: [
    WidgetComponent,
  ]
})
export class WidgetFrecuenciaComponent  implements OnInit {


  protected ritmoCardiaco: number = 0;
  constructor() { }

  ngOnInit() {}

}
