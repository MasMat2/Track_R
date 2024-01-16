import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GestionAsistenteComponent } from './gestion-asistente.component';
import { ModalBaseModule } from '@sharedComponents/modal-base/modal-base.module';

@NgModule({
  imports: [
    CommonModule,
    ModalBaseModule
  ],
  declarations: [GestionAsistenteComponent]
})
export class GestionAsistenteModule { }
