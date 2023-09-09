import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TabuladorEntidadModule } from '@sharedComponents/tabulador-entidad/tabulador-entidad.module';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { DashboardPadecimientoModule } from '../dashboard-padecimiento/dashboard-padecimiento.module';
import { ExpedienteEstudioModule } from '../expediente-estudio/expediente-estudio.module';
import { ExpedientePadecimientoModule } from '../expediente-padecimiento/expediente-padecimiento.module';
import { ExpedienteRecomendacionModule } from '../expediente-recomendacion/expediente-recomendacion.module';
import { ExpedienteTratamientoModule } from '../expediente-tratamiento/expediente-tratamiento.module';
import { ExpedienteGeneralFormularioModule } from './../expediente-general-formulario/expediente-general-formulario.module';
import { ExpedienteFormularioComponent } from './expediente-formulario.component';


@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DirectiveModule,
    ExpedienteGeneralFormularioModule,
    TabuladorEntidadModule,
    ExpedienteEstudioModule,
    DashboardPadecimientoModule,
    ExpedientePadecimientoModule,
    ExpedienteRecomendacionModule,
    ExpedienteTratamientoModule
  ],
  declarations: [ExpedienteFormularioComponent]
})
export class ExpedienteFormularioModule { }
