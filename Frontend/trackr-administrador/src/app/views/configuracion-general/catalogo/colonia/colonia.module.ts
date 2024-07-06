import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ColoniaService } from '@http/catalogo/colonia.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ColoniaFormularioComponent } from './colonia-formulario/colonia-formulario.component';
import { ColoniaRoutingModule } from './colonia-routing.module';
import { ColoniaComponent } from './colonia.component';
import { LucideAngularModule, X } from 'lucide-angular';

@NgModule({
  declarations: [
    ColoniaComponent,
    ColoniaFormularioComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    DirectiveModule,
    ModalModule.forChild(),
    ColoniaRoutingModule,
    LucideAngularModule.pick({X}),

  ],
  providers: [
    ColoniaService
  ],
  entryComponents: [
    ColoniaFormularioComponent
  ]
})
export class ColoniaModule { }
