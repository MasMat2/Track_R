import { CommonModule } from '@angular/common';
import { LOCALE_ID, NgModule } from '@angular/core';
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
import { LucideAngularModule } from 'lucide-angular';
import { CapitalizePipe } from 'src/app/shared/pipes/capitalize.pipe';
import { MAT_RIPPLE_GLOBAL_OPTIONS, MatRippleModule, RippleGlobalOptions } from '@angular/material/core';


@NgModule({
  declarations: [
    PacienteComponent,
    PacienteVistaCuadriculaComponent,
    CapitalizePipe
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
    LucideAngularModule,
    MatRippleModule
  ],
  entryComponents: [
    PacienteVistaCuadriculaComponent,
    ExpedienteFormularioComponent,
  ],
  exports: [],
  providers: [ 
    { provide: LOCALE_ID, useValue: 'es-MX'}
  ],
})
export class PacienteModule {}
