import { DomicilioFormularioModule } from '@sharedComponents/domicilio-formulario/domicilio-formulario.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { GridGeneralModule } from './components/grid-general/grid-general.module';
import { NgSelectModule } from '@ng-select/ng-select';

import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

//componentes
import { SpinnerComponent } from './spinner/spinner.component';

@NgModule({
  imports: [
    MatProgressSpinnerModule
  ],
  declarations: [
    SpinnerComponent
   ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    GridGeneralModule,
    NgSelectModule,
    DomicilioFormularioModule,
    SpinnerComponent,
    MatProgressSpinnerModule
  ],
  providers: []
})
export class SharedModule {
  constructor() {
  }
}
