import { DomicilioFormularioModule } from '@sharedComponents/domicilio-formulario/domicilio-formulario.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { GridGeneralModule } from './components/grid-general/grid-general.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { PdfVisorModule } from '@sharedComponents/pdf-visor/pdf-visor.module';

import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';


//componentes
import { SpinnerComponent } from './spinner/spinner.component';
import { CustomAlertComponent } from './components/custom-alert/custom-alert.component';

@NgModule({
  imports: [
    MatProgressSpinnerModule,
    MatDialogModule,
    MatButtonModule,
    CommonModule

  ],
  declarations: [
    SpinnerComponent,
    CustomAlertComponent
   ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    GridGeneralModule,
    NgSelectModule,
    DomicilioFormularioModule,
    PdfVisorModule,
    SpinnerComponent,
    MatProgressSpinnerModule,
    CustomAlertComponent
  ],
  providers: []
})
export class SharedModule {
  constructor() {
  }
}
