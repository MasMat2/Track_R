import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalModule } from '@coreui/angular';
import { CatalogoBaseModule } from '@sharedComponents/crud/catalogo-base/catalogo-base.module';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { RespuestaClasificacionPreguntaFormularioModule } from './respuestas-clasificacion-pregunta-formulario/respuesta-clasificacion-pregunta-formulario.module';
import { RespuestaClasificacionPreguntaRoutingModule } from './respuesta-clasificacion-pregunta-routing.module';
import { RespuestasClasificacionPreguntaComponent } from './respuestas-clasificacion-pregunta.component';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';

@NgModule({
  declarations: [RespuestasClasificacionPreguntaComponent],
  imports: [
    CommonModule,
    CatalogoBaseModule,
    DirectiveModule,
    ModalModule,
    GridGeneralModule,
    RespuestaClasificacionPreguntaFormularioModule,
    RespuestaClasificacionPreguntaRoutingModule
  ],
  exports:[RespuestasClasificacionPreguntaComponent]
})
export class RespuestaClasificacionPreguntaModule { }
