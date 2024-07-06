import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { ProgramacionExamenFormularioComponent } from './programacion-examen-formulario.component';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { LucideAngularModule, X } from 'lucide-angular';

@NgModule({
  declarations: [ProgramacionExamenFormularioComponent],
  imports: [
    CommonModule,
    FormsModule,
    GridGeneralModule,
    NgSelectModule,
    BsDatepickerModule,
    LucideAngularModule.pick({X}),

  ],
  exports: [],
  providers: [],
})
export class ProgramacionExamenFormularioModule {}
