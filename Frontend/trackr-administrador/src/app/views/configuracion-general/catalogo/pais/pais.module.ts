import { NgModule } from '@angular/core';
import { PaisService } from '@http/catalogo/pais.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { PaisFormularioComponent } from './pais-formulario/pais-formulario.component';
import { PaisComponent } from './pais.component';
import { PaisRoutingModule } from './pais.routing.module';
import { LucideAngularModule, X } from 'lucide-angular';

@NgModule({
  imports: [
    PaisRoutingModule,
    SharedModule,
    DirectiveModule,
    ModalModule.forChild(),
    LucideAngularModule.pick({X}),

  ],
  declarations: [
    PaisComponent,
    PaisFormularioComponent
  ],
  entryComponents: [PaisFormularioComponent],
  providers: [
    PaisService
  ]
})
export class PaisModule {}
