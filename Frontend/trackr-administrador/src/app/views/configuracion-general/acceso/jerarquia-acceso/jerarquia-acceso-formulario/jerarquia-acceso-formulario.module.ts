import { NgModule } from "@angular/core";
import { TreeModule } from "@circlon/angular-tree-component";
import { TipoCompaniaService } from "@http/catalogo/tipo-compania.service";
import { JerarquiaAccesoEstructuraService } from "@http/seguridad/jerarquia-acceso-estructura.service";
import { JerarquiaAccesoService } from "@http/seguridad/jerarquiaAcceso.service";
import { NgSelectModule } from "@ng-select/ng-select";
import { AngularTreeGridModule } from "angular-tree-grid";
import { ModalModule } from "ngx-bootstrap/modal";
import { DirectiveModule } from "src/app/shared/directives/directive.module";
import { SharedModule } from "src/app/shared/shared.module";
import { JerarquiaAccesoFormularioComponent } from "./jerarquia-acceso-formulario.component";
// import { DropDownTreeModule } from "@sharedComponents/drop-down-tree/drop-down-tree.module";

@NgModule({
    declarations: [JerarquiaAccesoFormularioComponent],
    imports: [
        SharedModule,
        DirectiveModule,
        NgSelectModule,
        TreeModule,
        // DropDownTreeModule,
        AngularTreeGridModule,
        ModalModule.forChild(),
    ],
    entryComponents: [JerarquiaAccesoFormularioComponent],
    providers: [
        JerarquiaAccesoService,
        JerarquiaAccesoEstructuraService,
        TipoCompaniaService
    ]
})
export class JerarquiaAccesoFormularioModule {}
