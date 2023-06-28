import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NivelExamenFormularioComponent } from './nivel-examen-formulario.component';
import { FormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
  declarations: [NivelExamenFormularioComponent],
  imports: [
    CommonModule,
    FormsModule,
    BsDatepickerModule,
  ],
  exports: [],
  providers: [],
})
export class NivelExamenFormularioModule {}
