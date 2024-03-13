import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { WidgetComponent } from '../widget/widget.component';
import { HealthConnectService } from 'src/app/services/dashboard/health-connect.service';
import { GetRecordsOptions, StoredRecord } from '../../interfaces/healthconnect-interfaces';

@Component({
  selector: 'app-widget-peso',
  templateUrl: './widget-peso.component.html',
  styleUrls: ['./widget-peso.component.scss'],
  standalone: true,
  
  imports: [
    WidgetComponent,
    IonicModule,
  ]
})
export class WidgetPesoComponent  implements OnInit {

  protected pesoPerdido: number = 12;
  protected pesoFaltante: number = 5;
  protected dias: number = 40;

  protected pesoActual: string = '0';

  constructor(private healthConnectservice : HealthConnectService) { }

  ngOnInit() {
    this.readRecordsWeight();
  }

  updateDataWeight(){
    this.readRecordsWeight();
  }

  async readRecordsWeight(): Promise<void> {
    const current = new Date();
    const startTime6months = new Date(current);
    startTime6months.setMonth(startTime6months.getMonth() - 6);
    const options : GetRecordsOptions = {
      type: 'Weight',
      timeRangeFilter: {
        type: 'between',
        startTime: startTime6months, //se obtendra todos los datos de pesos desde los ultimos 6 meses
        endTime: current
      }
    }

    const res = await this.healthConnectservice.readRecords(options)
    if (res && res.records && res.records.length > 0) {
      const ultimoRegistro : StoredRecord = res.records[res.records.length - 1];
      
      if (ultimoRegistro.weight!.value && ultimoRegistro.weight!.value !== undefined) {
        const ultimoPeso = ultimoRegistro.weight!.value;

        this.pesoActual = (ultimoPeso/1000).toString();
        console.log("Último peso registrado:", ultimoPeso);
      } else {
          console.log("No se encontró información de peso en el último registro.");
      }
    } else {
        console.log("No hay registros de peso.");
    }
  }

}
