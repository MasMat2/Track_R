import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpedientePadecimientoComponent } from './expediente-padecimiento.component';
import { SharedModule } from 'src/app/shared/shared.module';
import {MatExpansionModule} from '@angular/material/expansion';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    MatExpansionModule
  ],
  declarations: [ExpedientePadecimientoComponent],
  exports: [ExpedientePadecimientoComponent]
})
export class ExpedientePadecimientoModule { }
