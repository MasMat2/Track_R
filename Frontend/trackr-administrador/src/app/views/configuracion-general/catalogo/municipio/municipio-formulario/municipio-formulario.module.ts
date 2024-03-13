import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalBaseModule } from '@sharedComponents/modal-base/modal-base.module';
import { MunicipioFormularioComponent } from './municipio-formulario.component';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';

@NgModule({
  declarations: [MunicipioFormularioComponent],
  imports: [
    CommonModule,
    FormsModule,
    ModalBaseModule,
    NgSelectModule
  ],
  exports: [],
  providers: [],
})
export class MunicipioformularioModule {}
