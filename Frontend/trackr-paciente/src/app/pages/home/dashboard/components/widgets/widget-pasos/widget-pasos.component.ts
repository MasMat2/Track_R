import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { HealthkitService } from '@services/healthkit.service';


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

  //protected pasos: number = 15_000;
  protected unidadMedida: string = "pasos";
  protected meta: number = 10_000;

  protected metros: number = 10_000;
  protected horas: string = '-';
  protected minutos: string = '-';

  protected pasos : String = "0";
  protected distancia: number;

  constructor(private healthKitService: HealthkitService) {
    addIcons({
      'pasos': 'assets/img/svg/Pasos.svg'
    })
   }

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

      const timempoActual = new Date();
      const tiempoInicioUltima = new Date(ultimaCaminata.startDate);

      const diferenciaEnMilisegundos = timempoActual.getTime() - tiempoInicioUltima.getTime();

      const horasUltimaCaminata = Math.floor(diferenciaEnMilisegundos / (1000 * 60 * 60));
      const minutosUltimaCaminata = Math.floor((diferenciaEnMilisegundos % (1000 * 60 * 60)) / (1000 * 60));

      this.horas = horasUltimaCaminata.toString();
      this.minutos = minutosUltimaCaminata.toString();

    } catch (error) {
      console.error('Error al obtener datos de pasos:', error);
    }
  }


}
