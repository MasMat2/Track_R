import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { TableModule } from 'primeng/table';
import { PipesModule } from 'src/app/shared/pipes/pipes.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { DashboardPadecimientoModule } from './dashboard-padecimiento/dashboard-padecimiento.module';
import { ExpedienteEstudioModule } from './expediente-estudio/expediente-estudio.module';
import { ExpedienteFormularioComponent } from './expediente-formulario/expediente-formulario.component';
import { ExpedienteFormularioModule } from './expediente-formulario/expediente-formulario.module';
import { ExpedientePadecimientoModule } from './expediente-padecimiento/expediente-padecimiento.module';
import { ExpedienteTratamientoModule } from './expediente-tratamiento/expediente-tratamiento.module';
import { PacienteRoutingModule } from './paciente-routing.module';
import { PacienteVistaCuadriculaComponent } from './paciente-vista-cuadricula/paciente-vista-cuadricula.component';
import { PacienteComponent } from './paciente.component';
import { ExpedienteConsumoMedicamentoModule } from './expediente-consumo-medicamento/expediente-consumo-medicamento.module';
import { GestionAsistenteModule } from './gestion-asistente/gestion-asistente.module';
import { ChevronDown, Filter, LucideAngularModule, Plus, Search, Tally1 } from 'lucide-angular';

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
    DashboardPadecimientoModule,
    ExpedientePadecimientoModule,
    MatSidenavModule,
    MatIconModule,
    ExpedienteTratamientoModule,
    ExpedienteConsumoMedicamentoModule,
    GestionAsistenteModule,
    LucideAngularModule.pick({Filter, Tally1, Plus, ChevronDown, Search}),
  ],
  entryComponents: [
    PacienteVistaCuadriculaComponent,
    ExpedienteFormularioComponent,
  ],
  exports: [],
  providers: [],
})
export class PacienteModule {}
