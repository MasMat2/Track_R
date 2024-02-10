import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { HealthkitService } from '@services/healthkit.service';


@Component({
  selector: 'app-widget-sueno',
  templateUrl: './widget-sueno.component.html',
  styleUrls: ['./widget-sueno.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    WidgetComponent,
    IonicModule
  ]
})
export class WidgetSuenoComponent  implements OnInit {

  constructor(private healthKitService: HealthkitService) { }

  protected suenoActual: number = 78;
  protected horasMinDiarias: number = 1;
  protected minutosMinDiarias: number = 48;
  protected tiempoDormido: number = 6;
  protected minutostiempoDormido: number = 33;
  protected suenoProfundo: number = 2;
  protected minsuenoProfundo: number = 22;
  ngOnInit() {
    this.cargarDatos();
  }

  async cargarDatos() {
    try {
      
      
      const dataSleep = await this.healthKitService.getActivitySleep();

      //Se hace el conteo total de las horas en AsLeep (dormido incluyendo REM(Movimientos oculares rapidos) y Deep)
      const registrosDormidos = dataSleep.resultData.filter(registro => registro.sleepState === "Asleep");

      //Duración total de sueño en minutos
      const duracionTotalEnMinutos = Math.ceil(registrosDormidos.reduce((total, registro) => {
        const startDate = new Date(registro.startDate);
        const endDate = new Date(registro.endDate);
        const duracionEnMinutos = (endDate.getTime() - startDate.getTime()) / (1000 * 60); // Convertir de milisegundos a minutos
        return total + duracionEnMinutos;
      }, 0));

      this.tiempoDormido = Math.floor(duracionTotalEnMinutos / 60);
      this.minutostiempoDormido = duracionTotalEnMinutos % 60;

    } catch (error) {
      console.error('Error al obtener datos sleep Analysis:', error);
    }
  }

}
