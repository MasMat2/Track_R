import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { GridGeneralModule } from 'src/app/shared/components/grid-general/grid-general.module';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { NivelExamenFormularioModule } from './nivel-examen-formulario/nivel-examen-formulario.module';
import { NivelExamenComponent } from './nivel-examen.component';
import { NivelExamenRoutingModule } from './nivel-examen.routing.module';

@NgModule({
    imports: [
      CommonModule,
      GridGeneralModule,
      DirectiveModule,
      ModalModule.forChild(),
      NivelExamenRoutingModule,
      NivelExamenFormularioModule,
    ],
    declarations: [NivelExamenComponent],
    providers: []
  })
  export class NivelExamenModule {}
