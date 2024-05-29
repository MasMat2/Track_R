import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedModule, TabsModule } from '@coreui/angular';
import { ModalBaseModule } from '@sharedComponents/modal-base/modal-base.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { ClasificacionPreguntaFormularioComponent } from './clasificacion-pregunta-formulario.component';
import { RespuestaClasificacionPreguntaModule } from './respuestas-clasificacion-pregunta/respuesta-clasificacion-pregunta.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { CatalogoBaseModule } from '@sharedComponents/crud/catalogo-base/catalogo-base.module';
import { RespuestasClasificacionPreguntaComponent } from './respuestas-clasificacion-pregunta/respuestas-clasificacion-pregunta.component'; // Add this line

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ModalBaseModule,
    NgSelectModule,
    TabsModule,
    CatalogoBaseModule,
    MatExpansionModule,
    RespuestaClasificacionPreguntaModule // Import RespuestaClasificacionPreguntaModule here
  ],
  providers:[],
  declarations: [ClasificacionPreguntaFormularioComponent],
})
export class ClasificacionPreguntaFormularioModule { }

