import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { HealthConnectService } from 'src/app/services/dashboard/health-connect.service';
import { GetRecordsOptions } from '../../interfaces/healthconnect-interfaces';

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

  protected totalSleep: number = 0;
  protected sleep: number = 0;
  protected sleepREM: number = 0;
  protected sleepDeep: number = 0;

  constructor(private healthConnectservice : HealthConnectService) { }

  protected totalSuenoHoras: number = 0;
  protected totalSuenoMin: number = 0;
  protected totalSuenoREMHoras: number = 0;
  protected totalSuenoREMMin: number = 0;
  protected totalSuenoDeepHoras: number = 0;
  protected totalSuenoDeepMin: number = 0;


  ngOnInit() {
    this.readRecordsSleepSession();
  }

  updateDataSleepSession() {
    this.readRecordsSleepSession();
  }

  async readRecordsSleepSession(): Promise<void> {
    const current = new Date();
    const diaAnterior = new Date();
    diaAnterior.setHours(19, 0, 0, 0);
    diaAnterior.setDate(current.getDate() - 1); //Asi obtenermos el dia anterior a las 7 PM 
    
    const options : GetRecordsOptions = {
      type: 'SleepSession',
      timeRangeFilter: {
        type: 'between',
        startTime: diaAnterior, //se obtendra todos los datos de sleep desde el dia anterior a las 7PM
        endTime: current
      }
    }

    const res = await this.healthConnectservice.readRecords(options)
    console.log(JSON.stringify(res));

    this.sleep=0;
    this.sleepDeep=0;
    this.sleepREM=0;

    res.records.forEach(record => {
      if (record.type === 'SleepSession' && record.stages) {
          record.stages.forEach(stage => {
              const startTime = new Date(stage.startTime);
              const endTime = new Date(stage.endTime);
              const duration = (endTime.getTime() - startTime.getTime()) / 60000; // Convertir milisegundos a minutos

              switch (stage.stage) {
                  case 2:
                      this.sleep += duration;
                      break;
                  case 5:
                      this.sleepDeep += duration;
                      break;
                  case 6:
                      this.sleepREM += duration;
                      break;
              }
          });
      }
    });

    this.totalSuenoHoras = Math.floor(this.sleep / 60);
    this.totalSuenoMin = this.sleep % 60;

    this.totalSuenoREMHoras = Math.floor(this.sleepREM / 60);
    this.totalSuenoREMMin = this.sleepREM % 60;

    this.totalSuenoDeepHoras = Math.floor(this.sleepDeep / 60);
    this.totalSuenoDeepMin = this.sleepDeep % 60;

  }

}
