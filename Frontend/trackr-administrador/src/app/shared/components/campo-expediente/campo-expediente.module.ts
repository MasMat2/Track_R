import { NgModule } from '@angular/core';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { CommonModule } from '@angular/common';
import { CampoExpedienteComponent } from './campo-expediente.component';
import { SharedModule } from '../../shared.module';
import { NumberInputModule } from '../number-input/number-input.module';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
  imports: [
    SharedModule,
    CommonModule,
    BsDatepickerModule,
    PopoverModule,
    NumberInputModule,
  ],
  declarations: [CampoExpedienteComponent],
  exports: [CampoExpedienteComponent],
  providers: [],
})
export class CampoExpedienteModule { }
