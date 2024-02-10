import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { HealthData } from '../../../../../shared/Dtos/health-data/health-data-interface';
import { HealthService } from '@services/health.service';
import { HealthkitService } from '@services/healthkit.service';


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

  protected pasos : String = "0";
  protected distancia: number;

  constructor(private healthKitService: HealthkitService) { }

  ngOnInit() {
   this.cargarPasos();
  }

  async cargarPasos() {
    try {
      const dataPasos = await this.healthKitService.getSteps();
      //Sumar todos los registros de hoy en pasos
      const sumaPasos = dataPasos.resultData.reduce((total, elemento) => total + elemento.value, 0);
      this.pasos = sumaPasos.toString();
      
      //Obtener la diferencia entre la penultima caminata y la ultima caminata
      const penultimaCaminata = dataPasos.resultData[dataPasos.resultData.length - 2];
      const ultimaCaminata = dataPasos.resultData[dataPasos.resultData.length - 1];

      const fechaFinPenultima = new Date(penultimaCaminata.endDate);
      const fechaInicioUltima = new Date(ultimaCaminata.startDate);

      const diferenciaEnMilisegundos = fechaInicioUltima.getTime() - fechaFinPenultima.getTime();

      const horasUltimaCaminata = Math.floor(diferenciaEnMilisegundos / (1000 * 60 * 60));
      const minutosUltimaCaminata = Math.floor((diferenciaEnMilisegundos % (1000 * 60 * 60)) / (1000 * 60));

      this.horas = horasUltimaCaminata;
      this.minutos = minutosUltimaCaminata;

    } catch (error) {
      console.error('Error al obtener datos de pasos:', error);
    }
  }


}
