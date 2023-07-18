import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TableModule } from 'primeng/table';
import { PipesModule } from 'src/app/shared/pipes/pipes.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ExpedienteEstudioComponent } from './expediente-estudio/expediente-estudio.component';
import { ExpedienteEstudioModule } from './expediente-estudio/expediente-estudio.module';
import { ExpedienteFormularioComponent } from './expediente-formulario/expediente-formulario.component';
import { ExpedienteFormularioModule } from './expediente-formulario/expediente-formulario.module';
import { PacienteRoutingModule } from './paciente-routing.module';
import { PacienteVistaCuadriculaComponent } from './paciente-vista-cuadricula/paciente-vista-cuadricula.component';
import { PacienteComponent } from './paciente.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';

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
    ExpedienteFormularioModule,
    ExpedienteEstudioModule,
    MatSidenavModule,
    MatIconModule
  ],
  entryComponents: [
    PacienteVistaCuadriculaComponent,
    ExpedienteFormularioComponent,
  ],
  exports: [],
  providers: [],
})
export class PacienteModule {}
