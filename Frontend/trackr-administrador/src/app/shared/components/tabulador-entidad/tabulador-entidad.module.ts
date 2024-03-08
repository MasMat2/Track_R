import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTabsModule } from '@angular/material/tabs';
import { EntidadEstructuraTablaValorService } from '@http/gestion-entidad/entidad-estructura-tabla-valor.service';
import { EntidadEstructuraValorService } from '@http/gestion-entidad/entidad-estructura-valor.service';
import { EntidadEstructuraService } from '@http/gestion-entidad/entidad-estructura.service';
import { EntidadService } from '@http/gestion-entidad/entidad.service';
import { ModalBaseModule } from '@sharedComponents/modal-base/modal-base.module';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TableModule } from 'primeng/table';
import { CampoExpedienteModule } from '../campo-expediente/campo-expediente.module';
import { SeccionTablaFormularioComponent } from './seccion-tabla-formulario/seccion-tabla-formulario.component';
import { SeccionTablaComponent } from './seccion-tabla/seccion-tabla.component';
import { TabuladorEntidadComponent } from './tabulador-entidad.component';
import { TabModule } from './tab/tab.module';

@NgModule({
  imports: [
    CommonModule,
    TabsModule,
    CampoExpedienteModule,
    FormsModule,
    MatProgressSpinnerModule,
    MatTabsModule,
    MatIconModule,
    TableModule,
    ModalBaseModule,
    TabModule
  ],
  declarations: [
    TabuladorEntidadComponent,
    SeccionTablaComponent,
    SeccionTablaFormularioComponent
  ],
  entryComponents: [
    SeccionTablaFormularioComponent
  ],
  exports: [
    TabuladorEntidadComponent
  ],
  providers: [
    EntidadService,
    EntidadEstructuraService,
    EntidadEstructuraValorService,
    EntidadEstructuraTablaValorService
  ]
})
export class TabuladorEntidadModule { }
