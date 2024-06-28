import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';



@Component({
  selector: 'app-widget-pasos',
  templateUrl: './widget-pasos.component.html',
  styleUrls: ['./widget-pasos.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    WidgetComponent,
    IonicModule,
  ]
})
export class WidgetPasosComponent  implements OnInit {

  protected pasos: number = 0;
  protected unidadMedida: string = "pasos";

  constructor() {
    addIcons({
      'pasos': '/assets/img/svg/Pasos.svg'
    })
   }

  ngOnInit() {



  }

  updateDataSteps(){
    
  }

}
