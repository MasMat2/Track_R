import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CampoExpedienteComponent } from './campo-expediente.component';
import { NumberInputModule } from '../number-input/number-input.module';
import { SharedModule } from '@sharedComponents/shared.module';

@NgModule({
  imports: [
    SharedModule,
    CommonModule,
    // BsDatepickerModule,
    // PopoverModule,
    NumberInputModule,
  ],
  declarations: [CampoExpedienteComponent],
  exports: [CampoExpedienteComponent],
  providers: [],
})
export class CampoExpedienteModule { }
