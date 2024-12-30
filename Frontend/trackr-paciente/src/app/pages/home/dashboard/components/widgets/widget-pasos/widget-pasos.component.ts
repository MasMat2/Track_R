import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { GetRecordsOptions, HealthConnectAvailabilityStatus, StoredRecord } from '../../../interfaces/healthconnect-interfaces';
import { HealthConnectService } from 'src/app/services/dashboard/health-connect.service';
import { AlertController } from '@ionic/angular';

import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';




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

  private availability: HealthConnectAvailabilityStatus = "Unavailable"; //Disponibilidad de healthConnect

  protected meta: number = 0;

  protected metros: number = 10_000;
  protected horas: number = 0;
  protected minutos: number = 0;
  protected pasos: string = '0';
  protected unidadMedida: string = "pasos";

  constructor(
    private healthConnectservice : HealthConnectService,
    private alertController: AlertController ) {
      addIcons({
        'pasos': '/assets/img/svg/Pasos.svg'
      })
     }

  async ngOnInit() {
    
    this.healthConnectservice.setupComplete$.subscribe(isComplete => {
    
        if(isComplete == true){
          this.readRecordsSteps();
      
        }  
      }
    )
  }

  async updateDataSteps(){
    if(this.availability === "Available"){
      this.readRecordsSteps();
    } else {
      console.log('HealthConnect no disponible en este dispositivo, es necesario Android version 14')
      const alert = await this.alertController.create({
        header: 'No disponible',
        message: 'Funcionalidad solo disponible para Android 14 o superior',
        buttons: ['OK'],
      });
      
      await alert.present();
    }
  }

  async validarDisponibilidad(){
    const res = await this.healthConnectservice.checkAvailability();
    this.availability = res.availability;
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
