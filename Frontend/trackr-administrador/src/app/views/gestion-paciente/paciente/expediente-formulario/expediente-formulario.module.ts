import { ExpedienteGeneralFormularioModule } from './../expediente-general-formulario/expediente-general-formulario.module';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { ExpedienteFormularioComponent } from './expediente-formulario.component';
import { MatTabsModule } from '@angular/material/tabs';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TabuladorEntidadModule } from '@sharedComponents/tabulador-entidad/tabulador-entidad.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DirectiveModule,
    ExpedienteGeneralFormularioModule,
    TabuladorEntidadModule
  ],
  declarations: [ExpedienteFormularioComponent]
})
export class ExpedienteFormularioModule { }
