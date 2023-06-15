import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { AsignaturaFormularioComponent } from "./asignatura-formulario.component";
import { FormsModule } from "@angular/forms";


@NgModule({
    imports: [
      CommonModule,
      FormsModule
      // TreeModule,
      // DropDownTreeModule,
      // NgSelectizeModule,
      // DirectiveModule,
      // Daterangepicker,
      // BsDatepickerModule
    ],
    declarations: [AsignaturaFormularioComponent],
    providers: []
  })
  export class AsignaturaFormularioModule {}
