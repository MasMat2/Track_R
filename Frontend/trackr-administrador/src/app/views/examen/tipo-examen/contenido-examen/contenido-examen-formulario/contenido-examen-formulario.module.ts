import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgSelectModule } from '@ng-select/ng-select';
import { ContenidoExamenFormularioComponent } from './contenido-examen-formulario.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [ContenidoExamenFormularioComponent],
  imports: [
    CommonModule,
    NgSelectModule,
    FormsModule,
  ],
  exports: [],
  providers: [],
})
export class ContenidoExamenFormularioModule {}
