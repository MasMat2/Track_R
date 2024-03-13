import { NgModule } from "@angular/core";
import { JerarquiaAccesoService } from "@http/seguridad/jerarquiaAcceso.service";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { ModalModule } from "ngx-bootstrap/modal";
import { SharedModule } from "src/app/shared/shared.module";
import { JerarquiaAccesoFormularioModule } from "./jerarquia-acceso-formulario/jerarquia-acceso-formulario.module";
import { JerarquiaAccesoComponent } from "./jerarquia-acceso.component";
import { JerarquiaAccesoRoutingModule } from "./jerarquia-acceso.routing.module";
import { CatalogoBaseModule } from "@sharedComponents/crud/catalogo-base/catalogo-base.module";

@NgModule({
    imports: [
        JerarquiaAccesoRoutingModule,
        CatalogoBaseModule,
        SharedModule,
        CollapseModule,
        ModalModule.forRoot(),
        JerarquiaAccesoFormularioModule
    ],
    declarations: [JerarquiaAccesoComponent],
    providers: [JerarquiaAccesoService],
    entryComponents: []
})
export class JerarquiaAccesoModule {}
