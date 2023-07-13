import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { SeccionCampoService } from '@http/gestion-entidad/seccion-campo.service';
import { NgChartsModule } from 'ng2-charts';
import { SharedModule } from 'src/app/shared/shared.module';
import { ExpedientePadecimientoComponent } from './expediente-padecimiento.component';
import {MatChipsModule} from '@angular/material/chips';


@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    MatExpansionModule,
    NgChartsModule,
    MatChipsModule,
  ],
  providers: [
    SeccionCampoService
  ],
  declarations: [ExpedientePadecimientoComponent],
  exports: [ExpedientePadecimientoComponent]
})
export class ExpedientePadecimientoModule { }
