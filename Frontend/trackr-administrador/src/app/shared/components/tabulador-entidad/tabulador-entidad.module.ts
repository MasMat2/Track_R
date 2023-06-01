import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TabuladorEntidadComponent } from './tabulador-entidad.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { EntidadEstructuraService } from '@http/gestion-entidad/entidad-estructura.service';
import { EntidadService } from '@http/gestion-entidad/entidad.service';
import { CampoExpedienteModule } from '../campo-expediente/campo-expediente.module';
import { FormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { EntidadEstructuraValorService } from '@http/gestion-entidad/entidad-estructura-valor.service';
import { MatTabsModule } from '@angular/material/tabs';
import {MatIconModule} from '@angular/material/icon';


@NgModule({
  imports: [
    CommonModule,
    TabsModule,
    CampoExpedienteModule,
    FormsModule,
    MatProgressSpinnerModule,
    MatTabsModule,
    MatIconModule
  ],
  declarations: [
    TabuladorEntidadComponent
  ],
  exports: [
    TabuladorEntidadComponent
  ],
  providers: [
    EntidadService,
    EntidadEstructuraService,
    EntidadEstructuraValorService
  ]
})
export class TabuladorEntidadModule { }
