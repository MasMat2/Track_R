import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClasificacionPreguntaRoutingModule } from './clasificacion-pregunta-routing.module';
import { CatalogoBaseModule } from '@sharedComponents/crud/catalogo-base/catalogo-base.module';
import { ClasificacionPreguntaComponent } from './clasificacion-pregunta.component';
import { ClasificacionPreguntaFormularioModule } from './clasificacion-pregunta-formulario/clasificacion-pregunta-formulario.module';
import { ModalBaseModule } from '@sharedComponents/modal-base/modal-base.module';
import { FormsModule } from '@angular/forms';
import { TabsModule } from '@coreui/angular';


@NgModule({
  declarations: [
    ClasificacionPreguntaComponent
  ],
  imports: [
    FormsModule,
    TabsModule,
    CommonModule,
    ModalBaseModule,
    ClasificacionPreguntaRoutingModule,
    CatalogoBaseModule,
    ClasificacionPreguntaFormularioModule
  ],
})
export class ClasificacionPreguntaModule { }
