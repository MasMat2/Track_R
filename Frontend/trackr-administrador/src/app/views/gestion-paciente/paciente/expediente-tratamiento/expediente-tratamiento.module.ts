import { PipesModule } from 'src/app/shared/pipes/pipes.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { ExpedienteTratamientoComponent } from './expediente-tratamiento.component';

@NgModule({
  imports: [
    CommonModule,
    TableModule,
    PipesModule,
    GridGeneralModule
  ],
  declarations: [ExpedienteTratamientoComponent],
  exports: [ExpedienteTratamientoComponent]
})
export class ExpedienteTratamientoModule { }
