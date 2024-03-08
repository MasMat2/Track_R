import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';



@Component({
  selector: 'app-widget-pasos',
  templateUrl: './widget-pasos.component.html',
  styleUrls: ['./widget-pasos.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    WidgetComponent,
  ]
})
export class WidgetPasosComponent  implements OnInit {

  //protected pasos: number = 15_000;
  protected meta: number = 10_000;

  protected metros: number = 10_000;
  protected horas: number = 10;
  protected minutos: number = 10;
  protected pasos: number = 0;

  protected distancia: number;

  constructor() { }

  ngOnInit() {


  }

  updateDataSteps(){
    
  }

}
