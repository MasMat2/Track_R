import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TipoExamenFormularioComponent } from './tipo-examen-formulario.component';
import { FormsModule } from '@angular/forms';
import { LucideAngularModule, X } from 'lucide-angular';

@NgModule({
  declarations: [TipoExamenFormularioComponent],
  imports: [
    CommonModule,
    FormsModule,
    LucideAngularModule.pick({X}),
  ],
  exports: [],
  providers: [],
})
export class TipoExamenFormularioModule {}
