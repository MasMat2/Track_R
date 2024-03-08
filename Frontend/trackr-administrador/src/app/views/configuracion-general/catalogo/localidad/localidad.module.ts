import { NgModule } from '@angular/core';
import { EstadoService } from '@http/catalogo/estado.service';
import { LocalidadService } from '@http/catalogo/localidad.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { LocalidadFormularioComponent } from './localidad-formulario/localidad-formulario.component';
import { LocalidadComponent } from './localidad.component';
import { LocalidadRoutingModule } from './localidad.routing.module';

@NgModule({
    imports: [
      LocalidadRoutingModule,
      SharedModule,
      DirectiveModule,
      ModalModule.forChild()
    ],
    declarations: [
      LocalidadComponent,
      LocalidadFormularioComponent
    ],
    entryComponents: [LocalidadFormularioComponent],
    providers: [
      LocalidadService,
      MunicipioService,
      EstadoService,
      PaisService
    ]
  })

export class LocalidadModule {}
