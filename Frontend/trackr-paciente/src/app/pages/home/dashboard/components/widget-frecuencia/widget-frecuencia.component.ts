import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { HealthkitService } from '@services/healthkit.service';

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


  protected ritmoCardiaco: number = 100;
  constructor(private healthKitService: HealthkitService) { }

  ngOnInit() {
    this.cargarHR();
  }

  async cargarHR() {
    try {
      const dataHR = await this.healthKitService.getHR();
      console.log(dataHR);
      //Sumar todos los registros de hoy en ritmo cardiaco

      //const sumaHR = dataHR.resultData.reduce((total, elemento) => total + elemento.value, 0);
      //this.ritmoCardiaco = sumaHR;
    } catch (error) {
      console.error('Error al obtener datos de ritmo cardiaco:', error);
    }
  }

}
