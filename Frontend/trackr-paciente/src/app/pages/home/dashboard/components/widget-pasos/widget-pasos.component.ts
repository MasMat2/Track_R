import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { GetRecordsOptions, StoredRecord } from '../../interfaces/healthconnect-interfaces';
import { HealthConnectService } from 'src/app/services/dashboard/health-connect.service';



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
  protected horas: number = 0;
  protected minutos: number = 0;
  protected pasos: string = '0';

  protected distancia: number;

  constructor(
    private healthConnectservice : HealthConnectService
  ) { }

  ngOnInit() {
    this.readRecordsSteps();
  }

  updateDataSteps(){
    this.readRecordsSteps();
  }

  async readRecordsSteps() : Promise<void>{

    const today = new Date(); // Obtener la fecha actual
    const startOfDay = new Date(today.getFullYear(), today.getMonth(), today.getDate()); // Medianoche del día actual
    const endOfDay = new Date(today.getFullYear(), today.getMonth(), today.getDate() + 1); // Medianoche del día siguiente
    const options : GetRecordsOptions = {
      type: 'Steps',
      timeRangeFilter: {
        type: 'between',
        startTime: startOfDay,
        endTime: endOfDay
      }
    }
    
    const res = await this.healthConnectservice.readRecords(options)
    if (res && res.records && res.records.length > 0) {
      let totalSteps = 0;
  
      res.records.forEach(record => {
          totalSteps += record.count!;
      });

      const ultimoRegistro : StoredRecord = res.records[res.records.length - 1];
      const startTime = new Date(ultimoRegistro.startTime!);
      const endTime = new Date(ultimoRegistro.endTime!); //La suma de pasos desde la ultima media noche hasta medianoche del dia siguiente = totalSteps();
      console.log('Registros tiempo de pasos: '+startTime+' - '+endTime);
      const diferencia = endTime.getTime() - startTime.getTime();
      this.horas = Math.floor(diferencia / (1000 * 60 * 60));
      this.minutos = Math.floor((diferencia / (1000 * 60)) % 60);
      this.pasos = totalSteps.toString(); //La suma de pasos desde la ultima media noche hasta medianoche del dia siguiente
    } else {
        console.log("No se encontraron registros de pasos.");
    }
  }

}
