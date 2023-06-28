import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { EstadoFormularioModule } from './estado-formulario/estado-formulario.module';
import { EstadoComponent } from './estado.component';
import { EstadoRoutingModule } from './estado.routing.module';
import { CatalogoBaseModule } from '@sharedComponents/crud/catalogo-base/catalogo-base.module';

@NgModule({
  imports: [
    CommonModule,
    DirectiveModule,
    ModalModule.forChild(),
    GridGeneralModule,
    EstadoRoutingModule,
    EstadoFormularioModule,
    CatalogoBaseModule
  ],
  declarations: [
    EstadoComponent,
  ],
  // TODO: 2023-06-14 -> Ya no es necesario en la mayoría de los casos
  entryComponents: [],
  providers: []
})
export class EstadoModule {}
