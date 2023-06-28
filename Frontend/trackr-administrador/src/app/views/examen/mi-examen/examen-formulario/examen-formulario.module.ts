import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExamenFormularioComponent } from './examen-formulario.component';
import { ExamenFormularioRoutingModule } from './examen-formulario-routing.module';
import { TimerPipe } from './timer.pipe';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';

@NgModule({
  declarations: [
    ExamenFormularioComponent,
    TimerPipe
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgSelectModule,
    ExamenFormularioRoutingModule,
  ],
  exports: [],
  providers: [],
})
export class ExamenFormularioModule {}
