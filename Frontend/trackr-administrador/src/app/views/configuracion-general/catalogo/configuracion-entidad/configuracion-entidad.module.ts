import { NgModule } from "@angular/core";
import { EntidadService } from "@http/gestion-entidad/entidad.service";
import { CatalogoBaseModule } from "@sharedComponents/crud/components/catalogo-base/catalogo-base.module";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { ModalModule } from "ngx-bootstrap/modal";
import { DirectiveModule } from "src/app/shared/directives/directive.module";
import { SharedModule } from "src/app/shared/shared.module";
import { ConfiguracionEntidadFormularioModule } from "./configuracion-entidad-formulario/configuracion-entidad-formulario.module";
import { ConfiguracionEntidadComponent } from "./configuracion-entidad.component";
import { ConfiguracionEntidadRoutingModule } from "./configuracion-entidad.routing.module";

@NgModule({
  imports: [
    CatalogoBaseModule,
    ModalModule.forRoot(),
    CollapseModule,
    SharedModule,
    DirectiveModule,
    ConfiguracionEntidadRoutingModule,
    ConfiguracionEntidadFormularioModule
  ],
  declarations: [
    ConfiguracionEntidadComponent
  ],
  entryComponents: [],
  providers: [
    EntidadService
  ]
})
export class ConfiguracionEntidadModule {}
