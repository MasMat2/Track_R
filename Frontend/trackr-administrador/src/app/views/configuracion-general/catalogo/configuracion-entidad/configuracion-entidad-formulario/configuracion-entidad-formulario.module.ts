import { NgModule } from "@angular/core";
import { TreeModule } from "@circlon/angular-tree-component";
import { EntidadEstructuraService } from "@http/gestion-entidad/entidad-estructura.service";
import { EntidadService } from "@http/gestion-entidad/entidad.service";
import { SeccionCampoService } from "@http/gestion-entidad/seccion-campo.service";
import { SeccionService } from "@http/gestion-entidad/seccion.service";
import { NgSelectModule } from "@ng-select/ng-select";
import { AngularTreeGridModule } from "angular-tree-grid";
import { ModalModule } from "ngx-bootstrap/modal";
import { DirectiveModule } from "src/app/shared/directives/directive.module";
import { SharedModule } from "src/app/shared/shared.module";
import { ConfiguracionEntidadFormularioComponent } from "./configuracion-entidad-formulario.component";
import { SeccionCampoModalComponent } from "./seccion-campo-modal/seccion-campo-modal.component";
import { IconoService } from "@http/catalogo/icono.service";
import { WidgetService } from "@http/gestion-entidad/widgets-service";

@NgModule({
    declarations: [ConfiguracionEntidadFormularioComponent, SeccionCampoModalComponent],
    imports: [
        SharedModule,
        DirectiveModule,
        NgSelectModule,
        TreeModule,
        // DropDownTreeModule,
        AngularTreeGridModule,
        ModalModule.forChild(),
    ],
    entryComponents: [ConfiguracionEntidadFormularioComponent, SeccionCampoModalComponent],
    providers: [
        EntidadService,
        EntidadEstructuraService,
        SeccionCampoService,
        SeccionService,
        IconoService,
        WidgetService
    ]
})
export class ConfiguracionEntidadFormularioModule {}
