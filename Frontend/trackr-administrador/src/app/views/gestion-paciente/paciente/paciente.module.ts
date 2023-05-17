import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TableModule } from 'primeng/table';
import { PipesModule } from 'src/app/shared/pipes/pipes.module';
import { PacienteRoutingModule } from './paciente-routing.module';
import { PacienteComponent } from './paciente.component';

@NgModule({
  declarations: [
    PacienteComponent
  ],
  imports: [
    CommonModule,
    PipesModule,
    TableModule,
    PacienteRoutingModule
  ],
  exports: [],
  providers: [],
})
export class PacienteModule {}
