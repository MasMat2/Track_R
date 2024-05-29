import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { GridGeneralModule } from 'src/app/shared/components/grid-general/grid-general.module';
import { Reactivo1Component as Reactivo1Component } from './reactivo.component';
import { ReactivoRoutingModule as Reactivo1RoutingModule } from './reactivo.routing.module';
import { Reactivo1FormularioModule } from './reactivo-formulario/reactivo-formulario.module';

@NgModule({
  imports: [
    CommonModule,
    Reactivo1RoutingModule,
    GridGeneralModule,
    ModalModule.forChild(),
    Reactivo1FormularioModule
  ],
  declarations: [Reactivo1Component],
  providers: [],
})
export class ReactivoModule1 {}
