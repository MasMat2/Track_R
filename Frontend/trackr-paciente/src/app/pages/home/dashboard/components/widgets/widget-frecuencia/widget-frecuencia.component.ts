import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { HealthkitService } from '@services/healthkit.service';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';

@Component({
  selector: 'app-widget-frecuencia',
  templateUrl: './widget-frecuencia.component.html',
  styleUrls: ['./widget-frecuencia.component.scss'],
  standalone: true,
  imports: [
    WidgetComponent,
    IonicModule
  ]
})
export class WidgetFrecuenciaComponent  implements OnInit {


  protected ritmoCardiaco: string = '-';
  protected unidadMedida: string = "lpm";
  constructor(private healthKitService: HealthkitService) { 
    addIcons({
      'ritmo-cardiaco': '/assets/img/svg/Ritmo-cardiaco.svg'
    })
  }

  ngOnInit() {
    this.cargarHR();
  }

  async cargarHR() {
    try {
      const dataHR = await this.healthKitService.getHR();
      if (dataHR.resultData.length > 0) {
          const ultimoValor = dataHR.resultData[dataHR.resultData.length - 1].value;
          this.ritmoCardiaco = ultimoValor.toString();
      } else {
          this.ritmoCardiaco = "-";
      }

    } catch (error) {
      console.error('Error al obtener datos de ritmo cardiaco:', error);
    }
  }

}
