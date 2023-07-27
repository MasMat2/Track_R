import { NgModule } from '@angular/core';
import { EstadoService } from '@http/catalogo/estado.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { MunicipioFormularioComponent } from './municipio-formulario/municipio-formulario.component';
import { MunicipioComponent } from './municipio.component';
import { MunicipioRoutingModule } from './municipio.routing.module';

@NgModule({
    imports: [
      MunicipioRoutingModule,
      SharedModule,
      DirectiveModule,
      ModalModule.forChild()
    ],
    declarations: [
      MunicipioComponent,
      MunicipioFormularioComponent
    ],
    entryComponents: [MunicipioFormularioComponent],
    providers: [
      MunicipioService,
      EstadoService,
      PaisService
    ]
  })
  export class MunicipioModule {}
