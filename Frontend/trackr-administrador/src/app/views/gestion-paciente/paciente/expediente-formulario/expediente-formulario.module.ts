import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TabuladorEntidadModule } from '@sharedComponents/tabulador-entidad/tabulador-entidad.module';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ExpedienteEstudioModule } from '../expediente-estudio/expediente-estudio.module';
import { ExpedienteGeneralFormularioModule } from './../expediente-general-formulario/expediente-general-formulario.module';
import { ExpedienteFormularioComponent } from './expediente-formulario.component';
import { ExpedienteRecomendacionModule } from '../expediente-recomendacion/expediente-recomendacion.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DirectiveModule,
    ExpedienteGeneralFormularioModule,
    TabuladorEntidadModule,
    ExpedienteEstudioModule,
    ExpedienteRecomendacionModule
  ],
  declarations: [ExpedienteFormularioComponent]
})
export class ExpedienteFormularioModule { }
