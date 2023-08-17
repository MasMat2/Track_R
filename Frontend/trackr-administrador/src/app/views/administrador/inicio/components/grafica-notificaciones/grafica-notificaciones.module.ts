import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GraficaNotificacionesComponent } from './grafica-notificaciones.component';
import { NgChartsModule } from 'ng2-charts';

@NgModule({
  declarations: [GraficaNotificacionesComponent],
  imports: [
    CommonModule,
    NgChartsModule
  ],
  exports: [GraficaNotificacionesComponent],
  providers: [],
})
export class GraficaNotificacionesModule {}
