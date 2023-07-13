import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpedienteRecomendacionComponent } from './expediente-recomendacion.component';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    ExpedienteRecomendacionComponent
  ],
  exports: [
    ExpedienteRecomendacionComponent
  ],
  imports: [
    CommonModule,
    GridGeneralModule,
    FormsModule
  ]
})
export class ExpedienteRecomendacionModule { }
