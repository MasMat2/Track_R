import { PipesModule } from 'src/app/shared/pipes/pipes.module';
import { NgModule } from '@angular/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { ExpedienteTratamientoComponent } from './expediente-tratamiento.component';
import { NgChartsModule } from 'ng2-charts';

@NgModule({
  imports: [
    CommonModule,
    TableModule,
    MatExpansionModule,
    PipesModule,
    GridGeneralModule,
    NgChartsModule
  ],
  declarations: [ExpedienteTratamientoComponent],
  exports: [ExpedienteTratamientoComponent]
})
export class ExpedienteTratamientoModule { }
