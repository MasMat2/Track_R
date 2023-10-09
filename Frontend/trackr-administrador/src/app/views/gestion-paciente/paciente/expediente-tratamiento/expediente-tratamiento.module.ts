import { PipesModule } from 'src/app/shared/pipes/pipes.module';
import { NgModule } from '@angular/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { ExpedienteTratamientoComponent } from './expediente-tratamiento.component';

@NgModule({
  imports: [
    CommonModule,
    TableModule,
    MatExpansionModule,
    PipesModule,
    GridGeneralModule
  ],
  declarations: [ExpedienteTratamientoComponent],
  exports: [ExpedienteTratamientoComponent]
})
export class ExpedienteTratamientoModule { }
