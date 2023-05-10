import { NgModule } from '@angular/core';
import { EstadoService } from '@http/catalogo/estado.service';
import { PaisService } from '@http/catalogo/pais.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { EstadoFormularioComponent } from './estado-formulario/estado-formulario.component';
import { EstadoComponent } from './estado.component';
import { EstadoRoutingModule } from './estado.routing.module';

@NgModule({
  imports: [
    EstadoRoutingModule,
    SharedModule,
    DirectiveModule,
    ModalModule.forChild()
  ],
  declarations: [
    EstadoComponent,
    EstadoFormularioComponent
  ],
  entryComponents: [EstadoFormularioComponent],
  providers: [
    EstadoService,
    PaisService
  ]
})
export class EstadoModule {}
