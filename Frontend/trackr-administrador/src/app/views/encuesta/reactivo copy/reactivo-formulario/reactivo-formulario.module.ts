import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Reactivo1FormularioComponent } from './reactivo-formulario.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { InputArchivoModule } from '@sharedComponents/input-archivo/input-archivo.module';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { ReactivoService } from '@http/examen/reactivo.service';
import { RespuestaService } from '@http/examen/respuesta.service';
import { Respuesta1FormularioComponent } from './respuesta-formulario/respuesta-formulario.component';
import { LucideAngularModule, X } from 'lucide-angular';

@NgModule({
  declarations: [Reactivo1FormularioComponent, Respuesta1FormularioComponent],
  imports: [
    CommonModule,
    FormsModule,
    NgSelectModule,
    BsDatepickerModule,
    InputArchivoModule,
    GridGeneralModule,
    LucideAngularModule.pick({X}),
  ],
  exports: [],
  providers: [
    RespuestaService,
    ReactivoService
  ],
})
export class Reactivo1FormularioModule {}
