import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AccesoService } from '@http/seguridad/acceso.service';
import { AyudaSeccionService } from '@http/seguridad/ayuda-seccion.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { AyudaSeccionFormularioComponent } from './ayuda-seccion-formulario/ayuda-seccion-formulario.component';
import { AyudaSeccionRoutingModule } from './ayuda-seccion-routing.module';
import { AyudaSeccionComponent } from './ayuda-seccion.component';

@NgModule({
  declarations: [AyudaSeccionComponent, AyudaSeccionFormularioComponent],
  imports: [
    CommonModule,
    SharedModule,
    DirectiveModule,
    ModalModule.forChild(),
    AyudaSeccionRoutingModule,
  ],
  providers: [ AyudaSeccionService, AccesoService],
  entryComponents: [AyudaSeccionFormularioComponent],
})
export class AyudaSeccionModule {}
