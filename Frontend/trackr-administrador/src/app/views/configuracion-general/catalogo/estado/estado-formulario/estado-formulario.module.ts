import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PaisService } from '@http/catalogo/pais.service';
import { NgSelectModule } from '@ng-select/ng-select';
import { EstadoFormularioComponent } from './estado-formulario.component';

@NgModule({
  declarations: [
    EstadoFormularioComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgSelectModule
  ],
  exports: [],
  providers: [
    PaisService,
  ],
})
export class EstadoFormularioModule {}
