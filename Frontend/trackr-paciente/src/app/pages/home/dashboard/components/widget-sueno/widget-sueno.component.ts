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
  ngOnInit() {}

  async cargarDatos() {
    const dataSleep = await this.healthKitService.getActivitySleep();
    console.log(dataSleep);
    //this.hora=dataSleep.resultData[0].duration.toString();
  }

}
