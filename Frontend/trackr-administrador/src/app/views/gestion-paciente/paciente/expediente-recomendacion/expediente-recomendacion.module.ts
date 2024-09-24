import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { ExpedienteRecomendacionComponent } from './expediente-recomendacion.component';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { FormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { LucideAngularModule } from 'lucide-angular';
import { MatButtonModule } from '@angular/material/button';


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
    FormsModule,
    MatExpansionModule,
    BsDatepickerModule,
    MatButtonModule,
    LucideAngularModule
  ]
})
export class ExpedienteRecomendacionModule { }
