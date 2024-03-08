import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GestionAsistenteComponent } from './gestion-asistente.component';
import { ModalBaseModule } from '@sharedComponents/modal-base/modal-base.module';
import { TableModule } from 'primeng/table';
import { PipesModule } from 'src/app/shared/pipes/pipes.module';

@NgModule({
  imports: [
    CommonModule,
    ModalBaseModule,
    TableModule,
    PipesModule
  ],
  declarations: [GestionAsistenteComponent]
})
export class GestionAsistenteModule { }
