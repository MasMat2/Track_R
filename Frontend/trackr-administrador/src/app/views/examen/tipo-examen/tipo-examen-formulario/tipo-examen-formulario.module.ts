import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TipoExamenFormularioComponent } from './tipo-examen-formulario.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [TipoExamenFormularioComponent],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [],
  providers: [],
})
export class TipoExamenFormularioModule {}
