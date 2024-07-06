import { NgModule } from '@angular/core';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { CodigoPostalFormularioComponent } from './codigo-postal-formulario/codigo-postal-formulario.component';
import { CodigoPostalComponent } from './codigo-postal.component';
import { CodigoPostalRoutingModule } from './codigo-postal.routing.module';
import { LucideAngularModule, X } from 'lucide-angular';

@NgModule({
  imports: [
    SharedModule,
    DirectiveModule,
    ModalModule.forChild(),
    CodigoPostalRoutingModule,
    LucideAngularModule.pick({X}),

  ],
  declarations: [
    CodigoPostalComponent,
    CodigoPostalFormularioComponent
  ],
    entryComponents: [ CodigoPostalFormularioComponent],
    providers: [
      CodigoPostalService,
      MunicipioService
    ]
})
export class CodigoPostalModule { }
