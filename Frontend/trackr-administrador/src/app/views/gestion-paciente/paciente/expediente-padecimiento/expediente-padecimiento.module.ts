import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { ExpedientePadecimientoComponent } from './expediente-padecimiento.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { TableModule } from 'primeng/table';


@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    MatExpansionModule,
    TableModule

  ],
  declarations: [ExpedientePadecimientoComponent],
  exports: [ExpedientePadecimientoComponent]
})
export class ExpedientePadecimientoModule { }
