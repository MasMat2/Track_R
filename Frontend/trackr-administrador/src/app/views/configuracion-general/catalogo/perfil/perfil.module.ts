import { NgModule } from "@angular/core";
import { TreeModule } from "@circlon/angular-tree-component";
import { CompaniaService } from "@http/catalogo/compania.service";
import { JerarquiaAccesoEstructuraService } from "@http/seguridad/jerarquia-acceso-estructura.service";
import { JerarquiaAccesoService } from "@http/seguridad/jerarquiaAcceso.service";
import { PerfilService } from "@http/seguridad/perfil.service";
import { NgSelectModule } from "@ng-select/ng-select";
import { GridGeneralModule } from "@sharedComponents/grid-general/grid-general.module";
import { DirectiveModule } from "src/app/shared/directives/directive.module";
import { SharedModule } from "src/app/shared/shared.module";
import { PerfilFormularioComponent } from "./perfil-formulario/perfil-formulario.component";
import { PerfilComponent } from "./perfil.component";
import { PerfilRoutingModule } from "./perfil.routing.module";

@NgModule({
  imports: [
    SharedModule,
    PerfilRoutingModule,
    GridGeneralModule,
    TreeModule,
    NgSelectModule,
    DirectiveModule,
  ],
  declarations: [PerfilComponent, PerfilFormularioComponent],
  entryComponents: [],
  providers: [
    PerfilService,
    CompaniaService,
    JerarquiaAccesoService,
    JerarquiaAccesoEstructuraService
  ],
})
export class PerfilModule {}
