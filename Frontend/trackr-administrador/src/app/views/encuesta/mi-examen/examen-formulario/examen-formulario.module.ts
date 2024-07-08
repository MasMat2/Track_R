import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { ExamenFormularioRoutingModule } from './examen-formulario-routing.module';
import { ExamenFormularioComponent } from './examen-formulario.component';
import { TimerPipe } from './timer.pipe';
import { CheckboxModule } from 'primeng/checkbox';

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
    CheckboxModule
  ],
  exports: [ExamenFormularioComponent],
  providers: [],
})
export class ExamenFormularioModule {}
