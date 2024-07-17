import { NgModule } from '@angular/core';
import { RolService } from '@http/seguridad/rol.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { RolFormularioComponent } from './rol-formulario/rol-formulario.component';
import { RolComponent } from './rol.component';
import { RolRoutingModule } from './rol.routing.module';
import { LucideAngularModule, X } from 'lucide-angular';

@NgModule({
  imports: [
    RolRoutingModule,
    SharedModule,
    DirectiveModule,
    LucideAngularModule.pick({X}),
    ModalModule.forChild(),

  ],
  declarations: [
    RolComponent,
    RolFormularioComponent
  ],
  entryComponents:[RolFormularioComponent ],
  providers:[RolService]
})
export class RolModule { }
