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
import { AyudaSeccionService } from '@http/seguridad/ayuda-seccion.service';
import { AccesoAyudaService } from "@http/seguridad/acceso-ayuda.service";
import { AccesoAyudaComponent } from "./acceso-ayuda/acceso-ayuda.component";
import { ReporteArbolAccesoComponent } from "./reporte-arbol-acceso/reporte-arbol-acceso.component";
import { AngularTreeGridModule } from "angular-tree-grid";
import { LucideAngularModule } from "lucide-angular";

@NgModule({
  imports: [
    SharedModule,
    AccesoRoutingModule,
    GridGeneralModule,
    DirectiveModule,
    CollapseModule,
    AngularTreeGridModule,
    ModalModule.forChild(),
    LucideAngularModule
  ],
  declarations: [
    AccesoComponent,
    AccesoFormularioComponent,
    AccesoAyudaComponent,
    ReporteArbolAccesoComponent
  ],
  providers: [
    AccesoService,
    TipoAccesoService,
    IconoService,
    RolAccesoService,
    AccesoAyudaService,
    AyudaSeccionService
  ],
  entryComponents: [AccesoFormularioComponent]
})
export class AccesoModule {}
