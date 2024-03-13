import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { EspecialidadFormularioModule } from './especialidad-formulario/especialidad-formulario.module';
import { EspecialidadComponent } from './especialidad.component';
import { EspecialidadRoutingModule } from './especialidad.routing.module';
import { CatalogoBaseModule } from '@sharedComponents/crud/catalogo-base/catalogo-base.module';

@NgModule({
  imports: [
    CommonModule,
    DirectiveModule,
    ModalModule.forChild(),
    GridGeneralModule,
    EspecialidadRoutingModule,
    EspecialidadFormularioModule,
    CatalogoBaseModule
  ],
  declarations: [
    EspecialidadComponent,
  ],

  entryComponents: [],
  providers: []
})
export class EspecialidadModule {}
