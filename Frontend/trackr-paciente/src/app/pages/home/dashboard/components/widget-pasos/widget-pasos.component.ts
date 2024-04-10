import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { HealthData } from '../../../../../shared/Dtos/health-data/health-data-interface';
import { HealthService } from '@services/health.service';


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
  protected meta: number = 0;

  protected metros: number = 0;
  protected horas: number = 0;
  protected minutos: number = 0;

  protected pasos : HealthData;
  protected distancia: number = 0;

  constructor(private healthService: HealthService) { }

  ngOnInit() {

    this.healthService.getPermissionState(); 


    this.healthService.getPasos().then(data => {
      this.pasos = data;
      this.distancia = Math.round(this.pasos.value*0.68);
    }).catch(error => {
      // handle error here
      console.error(error);
    });
  }

  updateDataSteps(){
    this.healthService.getPasos().then(data => {
      this.pasos = data;
      this.distancia = Math.round(this.pasos.value*0.68);
    }).catch(error => {
      // handle error here
      console.error(error);
    });
  }

}
