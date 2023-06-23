import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { GridGeneralModule } from 'src/app/shared/components/grid-general/grid-general.module';
import { TipoExamenFormularioModule } from './tipo-examen-formulario/tipo-examen-formulario.module';
import { TipoExamenComponent } from './tipo-examen.component';
import { TipoExamenRoutingModule } from './tipo-examen.routing.module';

@NgModule({
    imports: [
      CommonModule,
      TipoExamenRoutingModule,
      GridGeneralModule,
      ModalModule.forChild(),
      TipoExamenFormularioModule,
    ],
    declarations: [TipoExamenComponent],
    providers: []
  })
  export class TipoExamenModule {}
