import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GraficaTratamientosComponent } from './grafica-tratamientos.component';
import { NgChartsModule } from 'ng2-charts';

@NgModule({
  declarations: [GraficaTratamientosComponent],
  imports: [
    CommonModule,
    NgChartsModule
  ],
  exports: [GraficaTratamientosComponent],
  providers: [],
})
export class GraficaTratamientosModule {}
