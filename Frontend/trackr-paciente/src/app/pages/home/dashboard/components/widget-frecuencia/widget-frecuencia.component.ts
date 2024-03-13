import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { HealthConnectService } from 'src/app/services/dashboard/health-connect.service';
import { GetRecordsOptions } from '../../interfaces/healthconnect-interfaces';
import { RecordType } from 'capacitor-health-connect-local';


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


  protected ritmoCardiaco: number = 0;
  constructor(private healthConnectService: HealthConnectService) { }

  ngOnInit() {
    this.readRecordsHeartRate();
  }

  updateDataHeartRate(){
    this.readRecordsHeartRate();
  }


  async readRecordsHeartRate(): Promise<void> {
    const current = new Date();
    const startTime6months = new Date(current);
    startTime6months.setMonth(startTime6months.getMonth() - 6);
    const options : GetRecordsOptions = {
      type: 'HeartRateSeries' as RecordType, //Suprime el warning de que HeartRateSeries no es parte de RecordType, pero este se debe a que asi esta declarado en RecordsTypeNameMap.kt por Android.
      timeRangeFilter: {
        type: 'between',
        startTime: startTime6months, //se obtendra todos los datos de pesos desde los ultimos 6 meses
        endTime: current
      }
    }

    const res = await this.healthConnectService.readRecords(options)

    if (res && res.records && res.records.length > 0) {
      const ultimoRegistro = res.records[res.records.length - 1];
      
      this.ritmoCardiaco = ultimoRegistro.samples![0].beatsPerMinute;
    } else {
        console.log("No hay registros de HeartRate.");
    }
  }

}
