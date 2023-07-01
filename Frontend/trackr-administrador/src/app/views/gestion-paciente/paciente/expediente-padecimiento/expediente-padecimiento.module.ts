import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpedientePadecimientoComponent } from './expediente-padecimiento.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [ExpedientePadecimientoComponent],
  exports: [ExpedientePadecimientoComponent]
})
export class ExpedientePadecimientoModule { }
