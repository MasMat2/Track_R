import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { AsignaturaFormularioComponent } from "./asignatura-formulario.component";
import { FormsModule } from "@angular/forms";
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { LucideAngularModule, X } from "lucide-angular";
@NgModule({
    imports: [
      CommonModule,
      BsDatepickerModule,
      FormsModule,
      LucideAngularModule.pick({X})
    ],
    declarations: [AsignaturaFormularioComponent],
    providers: []
  })
  export class AsignaturaFormularioModule {}
