import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RespuestaClasificacionPreguntaModule } from '../respuesta-clasificacion-pregunta.module';
import { RespuestasClasificacionPreguntaFormularioComponent } from './respuestas-clasificacion-pregunta-formulario.component';
import { FormsModule } from '@angular/forms';
import { TabsModule } from '@coreui/angular';
import { NgSelectModule } from '@ng-select/ng-select';
import { CatalogoBaseModule } from '@sharedComponents/crud/catalogo-base/catalogo-base.module';
import { InputArchivoModule } from '@sharedComponents/input-archivo/input-archivo.module';
import { ModalBaseModule } from '@sharedComponents/modal-base/modal-base.module';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';



@NgModule({
  declarations: [RespuestasClasificacionPreguntaFormularioComponent],
  imports: [
    CommonModule,
    FormsModule,
    ModalBaseModule,
    NgSelectModule,
    TabsModule,
    InputArchivoModule,
    BsDatepickerModule,
    CatalogoBaseModule,
  ]
})
export class RespuestaClasificacionPreguntaFormularioModule { }
