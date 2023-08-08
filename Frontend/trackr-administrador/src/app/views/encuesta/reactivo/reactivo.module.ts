import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { GridGeneralModule } from 'src/app/shared/components/grid-general/grid-general.module';
import { ReactivoComponent } from './reactivo.component';
import { ReactivoRoutingModule } from './reactivo.routing.module';
import { ReactivoFormularioModule } from './reactivo-formulario/reactivo-formulario.module';

@NgModule({
  imports: [
    CommonModule,
    ReactivoRoutingModule,
    GridGeneralModule,
    ModalModule.forChild(),
    ReactivoFormularioModule
  ],
  declarations: [ReactivoComponent],
  providers: [],
})
export class ReactivoModule {}
