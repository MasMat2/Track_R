import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NivelExamenFormularioComponent } from './nivel-examen-formulario.component';
import { FormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { LucideAngularModule, X } from 'lucide-angular';

@NgModule({
  declarations: [NivelExamenFormularioComponent],
  imports: [
    CommonModule,
    FormsModule,
    BsDatepickerModule,
    LucideAngularModule.pick({X}),
  ],
  exports: [],
  providers: [],
})
export class NivelExamenFormularioModule {}
