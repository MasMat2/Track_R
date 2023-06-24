import { NgModule } from '@angular/core';
import { EstadoService } from '@http/catalogo/estado.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { CatalogoBaseModule } from '@sharedComponents/crud/catalogo-base/catalogo-base.module';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { MunicipioformularioModule } from './municipio-formulario/municipio-formulario.module';
import { MunicipioComponent } from './municipio.component';
import { MunicipioRoutingModule } from './municipio.routing.module';

@NgModule({
    imports: [
      SharedModule,
      DirectiveModule,
      ModalModule.forChild(),
      CatalogoBaseModule,
      MunicipioRoutingModule,
      MunicipioformularioModule
    ],
    declarations: [MunicipioComponent],
    providers: [
      MunicipioService,
      EstadoService,
      PaisService
    ]
  })
  export class MunicipioModule {}
