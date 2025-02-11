import { NgModule } from '@angular/core';
import { DominioDetalleService } from '@http/catalogo/dominio-detalle.service';
import { DominioService } from '@http/catalogo/dominio.service';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { DominioFormularioComponent } from './dominio-formulario/dominio-formulario.component';
import { DominioComponent } from './dominio.component';
import { DominioRoutingModule } from './dominio.routing.module';
import { DominioHospitalFormularioComponent } from './dominio-formulario/dominio-hospital-formulario/dominio-hospital-formulario.component';
import { LucideAngularModule, X } from 'lucide-angular';

@NgModule({
    imports: [
      DominioRoutingModule,
      SharedModule,
      DirectiveModule,
      BsDatepickerModule,
      CollapseModule,
      ModalModule.forChild(),
      LucideAngularModule.pick({X}),

    ],
    declarations: [
      DominioComponent,
      DominioFormularioComponent,
      DominioHospitalFormularioComponent
    ],
    entryComponents: [DominioFormularioComponent],
    providers: [
      DominioService,
      DominioDetalleService
    ]
  })
  export class DominioModule {}
