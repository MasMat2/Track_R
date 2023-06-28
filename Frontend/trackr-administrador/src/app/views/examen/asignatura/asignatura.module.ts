import { NgModule } from "@angular/core";
import { GridGeneralModule } from "@sharedComponents/grid-general/grid-general.module";
import { ModalModule } from "ngx-bootstrap/modal";
import { AsignaturaFormularioModule } from "./asignatura-formulario/asignatura-formulario.module";
import { AsignaturaComponent } from "./asignatura.component";
import { AsignaturaRoutingModule } from "./asignatura.routing.module";
import { CommonModule } from "@angular/common";


@NgModule({
    imports: [
      AsignaturaRoutingModule,
      GridGeneralModule,
      CommonModule,
      ModalModule.forChild(),
      AsignaturaFormularioModule

    ],
    declarations: [
        AsignaturaComponent,
    ],
    entryComponents: [],
    providers: []
  })
  export class AsignaturaModule {}
