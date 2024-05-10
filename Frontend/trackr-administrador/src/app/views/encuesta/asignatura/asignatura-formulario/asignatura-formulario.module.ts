import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { AsignaturaFormularioComponent } from "./asignatura-formulario.component";
import { FormsModule } from "@angular/forms";
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
@NgModule({
    imports: [
      CommonModule,
      BsDatepickerModule,
      FormsModule,
    ],
    declarations: [AsignaturaFormularioComponent],
    providers: []
  })
  export class AsignaturaFormularioModule {}
