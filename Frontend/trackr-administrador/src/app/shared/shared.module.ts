import { DomicilioFormularioModule } from '@sharedComponents/domicilio-formulario/domicilio-formulario.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { GridGeneralModule } from './components/grid-general/grid-general.module';
import { NgSelectModule } from '@ng-select/ng-select';

@NgModule({
  imports: [],
  declarations: [
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    GridGeneralModule,
    NgSelectModule,
    DomicilioFormularioModule
  ],
  providers: []
})
export class SharedModule {
  constructor() {
  }
}
