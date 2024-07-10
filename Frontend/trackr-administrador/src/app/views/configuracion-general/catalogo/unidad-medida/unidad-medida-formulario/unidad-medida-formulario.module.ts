import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UnidadMedidaFormularioComponent } from './unidad-medida-formulario.component';
import { ModalBaseModule } from '@sharedComponents/modal-base/modal-base.module';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';

@NgModule({
  imports: [
    CommonModule,
    ModalBaseModule,
    FormsModule,
    NgSelectModule
  ],
  declarations: [UnidadMedidaFormularioComponent]
})
export class UnidadMedidaFormularioModule { }
