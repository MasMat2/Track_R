import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecomendacionGeneralComponent } from './recomendacion-general.component';
import { FormsModule } from '@angular/forms';
import { MatExpansionModule } from '@angular/material/expansion';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { EncryptionService } from '@services/encryption.service';
import { ExpedienteRecomendacionGeneralService } from '@http/gestion-expediente/expediente-recomendacion-general.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { FormularioService } from '@services/formulario.service';
import { EntidadEstructuraService } from '@http/gestion-entidad/entidad-estructura.service';
import { ExpedienteTrackrService } from '@http/seguridad/expediente-trackr.service';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
  imports: [
    CommonModule, 
    FormsModule,
    MatExpansionModule,
    GridGeneralModule, 
    BsDatepickerModule,
    NgSelectModule
  ],
  declarations: [RecomendacionGeneralComponent],
  providers: [
    ExpedienteRecomendacionGeneralService,
    EncryptionService,
    MensajeService,
    FormularioService,
    EntidadEstructuraService,
    ExpedienteTrackrService
  ]
})
export class RecomendacionGeneralModule { }
