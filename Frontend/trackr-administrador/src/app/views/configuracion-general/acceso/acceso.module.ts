import { NgModule } from "@angular/core";
import { IconoService } from "@http/catalogo/icono.service";
import { RolAccesoService } from "@http/seguridad/acceso-rol.service";
import { AccesoService } from "@http/seguridad/acceso.service";
import { TipoAccesoService } from "@http/seguridad/tipo-acceso.service";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { ModalModule } from "ngx-bootstrap/modal";
import { GridGeneralModule } from "src/app/shared/components/grid-general/grid-general.module";
import { DirectiveModule } from "src/app/shared/directives/directive.module";
import { SharedModule } from "src/app/shared/shared.module";
import { AccesoFormularioComponent } from "./acceso-formulario/acceso-formulario.component";
import { AccesoComponent } from "./acceso.component";
import { AccesoRoutingModule } from "./acceso.routing.module";


@NgModule({
  imports: [
    SharedModule,
    AccesoRoutingModule,
    GridGeneralModule,
    DirectiveModule,
    CollapseModule,
    ModalModule.forChild()
  ],
  declarations: [
    AccesoComponent,
    AccesoFormularioComponent,
  ],
  providers: [
    AccesoService,
    TipoAccesoService,
    IconoService,
    RolAccesoService
  ],
  entryComponents: [AccesoFormularioComponent]
})
export class AccesoModule {}
