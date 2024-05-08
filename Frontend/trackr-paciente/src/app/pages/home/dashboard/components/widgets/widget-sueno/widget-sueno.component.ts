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
  
  //Estado de healthkit: AsLeep
  protected tiempoDormido: number = 0;
  protected minutostiempoDormido: number = 0;

  //Estado de healthkit: AsleepREM
  protected suenoHorasREM: number = 0;
  protected suenoMinutosREM: number = 0;
  
  //Estado de healthkit: AsleepDeep
  protected suenoHorasProfundo: number = 0;
  protected suenoMinutosProfundo: number = 0;
  ngOnInit() {
    this.cargarDatos();
  }

  async cargarDatos() {
    try {
      this.tiempoDormido = 0;
      this.minutostiempoDormido = 0;
      this.suenoHorasREM = 0;
      this.suenoMinutosREM = 0;
      this.suenoHorasProfundo = 0;
      this.suenoMinutosProfundo = 0;
      
      const dataSleep = await this.healthKitService.getActivitySleep();

      for (const data of dataSleep.resultData) {
        const sleepState = data.sleepState;
        
        const startDate = new Date(data.startDate);
        const endDate = new Date(data.endDate);
        const durationMs = endDate.getTime() - startDate.getTime();
        const durationMinutes = durationMs / (1000 * 60); 
        
        switch (sleepState) {
            case 'Asleep':
                this.tiempoDormido += Math.floor(durationMinutes / 60);
                this.minutostiempoDormido += Math.floor(durationMinutes % 60);
                break;
            case 'AsleepREM':
                this.suenoHorasREM += Math.floor(durationMinutes / 60);
                this.suenoMinutosREM += Math.floor(durationMinutes % 60);
                break;
            case 'AsleepDeep':
                this.suenoHorasProfundo += Math.floor(durationMinutes / 60);
                this.suenoMinutosProfundo += Math.floor(durationMinutes % 60); 
                break;
            default:
                console.log('Estado de sueño desconocido:', sleepState);
                break;
        }
    }
      

    } catch (error) {
      console.error('Error al obtener datos sleep Analysis:', error);
    }
  }

}
