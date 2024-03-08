import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ResumenPadecimientosComponent } from './resumen-padecimientos.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [ResumenPadecimientosComponent],
  imports: [CommonModule, NgSelectModule, FormsModule],
  exports: [ResumenPadecimientosComponent],
  providers: [],
})
export class ResumenPadecimientosModule {}
