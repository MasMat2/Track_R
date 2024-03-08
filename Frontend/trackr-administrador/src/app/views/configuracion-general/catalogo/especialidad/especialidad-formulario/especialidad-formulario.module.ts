import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EspecialidadService } from '@http/catalogo/especialidad.service';
import { NgSelectModule } from '@ng-select/ng-select';
import { EspecialidadFormularioComponent } from './especialidad-formulario.component';
import { ModalBaseModule } from '@sharedComponents/modal-base/modal-base.module';

@NgModule({
  declarations: [
    EspecialidadFormularioComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgSelectModule,
    ModalBaseModule,
  ],
  exports: [],
  providers: [
    EspecialidadService,
  ],
})
export class EspecialidadFormularioModule {}