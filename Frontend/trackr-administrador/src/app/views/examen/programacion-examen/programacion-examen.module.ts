import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { GridGeneralModule } from 'src/app/shared/components/grid-general/grid-general.module';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { ProgramacionExamenFormularioModule } from './programacion-examen-formulario/programacion-examen-formulario.module';
import { ProgramacionExamenComponent } from './programacion-examen.component';
import { ProgramacionExamenRoutingModule } from './programacion-examen.routing.module';

@NgModule({
  imports: [
    CommonModule,
    GridGeneralModule,
    DirectiveModule,
    ModalModule.forChild(),
    ProgramacionExamenRoutingModule,
    ProgramacionExamenFormularioModule,
  ],
  declarations: [ProgramacionExamenComponent],
  providers: [],
})
export class ProgramacionExamenModule {}
