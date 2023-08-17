import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GraficaPacientesComponent } from './grafica-pacientes.component';
import { NgChartsModule } from 'ng2-charts';

@NgModule({
  declarations: [GraficaPacientesComponent],
  imports: [
    CommonModule,
    NgChartsModule
  ],
  exports: [GraficaPacientesComponent],
  providers: [],
})
export class GraficaPacientesModule {}
