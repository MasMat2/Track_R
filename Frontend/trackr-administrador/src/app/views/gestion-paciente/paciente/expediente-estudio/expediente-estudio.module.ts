import { PdfVisorModule } from '@sharedComponents/pdf-visor/pdf-visor.module';
import { PipesModule } from 'src/app/shared/pipes/pipes.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { ExpedienteEstudioComponent } from './expediente-estudio.component';

@NgModule({
  imports: [
    CommonModule,
    TableModule,
    PipesModule,
    PdfVisorModule
  ],
  declarations: [ExpedienteEstudioComponent],
  exports: [ExpedienteEstudioComponent]
})
export class ExpedienteEstudioModule { }
