import { SharedModule } from 'src/app/shared/shared.module';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TableModule } from 'primeng/table';
import { PipesModule } from 'src/app/shared/pipes/pipes.module';
import { PacienteRoutingModule } from './paciente-routing.module';
import { PacienteVistaCuadriculaComponent } from './paciente-vista-cuadricula/paciente-vista-cuadricula.component';
import { PacienteComponent } from './paciente.component';
import { PacienteFormularioComponent } from './paciente-formulario/paciente-formulario.component';
import { PacienteFormularioModule } from './paciente-formulario/paciente-formulario.module';

@NgModule({
  declarations: [
    PacienteComponent,
    PacienteVistaCuadriculaComponent,
  ],
  imports: [
    SharedModule,
    CommonModule,
    PipesModule,
    TableModule,
    PacienteRoutingModule,
    PacienteFormularioModule
  ],
  entryComponents: [
    PacienteVistaCuadriculaComponent, 
    PacienteFormularioComponent,
  ],
  exports: [],
  providers: [],
})
export class PacienteModule {}
