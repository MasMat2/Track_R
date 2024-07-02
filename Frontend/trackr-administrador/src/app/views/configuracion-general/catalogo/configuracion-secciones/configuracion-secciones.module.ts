import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SeccionCampoService } from '@http/gestion-entidad/seccion-campo.service';
import { SeccionService } from '@http/gestion-entidad/seccion.service';
import { DominioService } from '@http/catalogo/dominio.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ConfiguracionSeccionesFormularioComponent } from './configuracion-secciones-formulario/configuracion-secciones-formulario.component';
import { ConfiguracionSeccionesComponent } from './configuracion-secciones.component';
import { ConfiguracionSeccionesRoutingModule } from './configuracion-secciones.routing.module';
import { IconoService } from '@http/catalogo/icono.service';
import { FormsModule } from '@angular/forms';
import { LucideAngularModule, X } from 'lucide-angular';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DirectiveModule,
    ModalModule.forChild(),
    ConfiguracionSeccionesRoutingModule,
    FormsModule,
    LucideAngularModule.pick({X}),

  ],
  declarations: [
    ConfiguracionSeccionesComponent,
    ConfiguracionSeccionesFormularioComponent
  ],
  entryComponents: [
    ConfiguracionSeccionesFormularioComponent
  ],
  providers: [
    SeccionCampoService,
    SeccionService,
    DominioService,
    IconoService
  ]
})
export class ConfiguracionSeccionesModule { }
