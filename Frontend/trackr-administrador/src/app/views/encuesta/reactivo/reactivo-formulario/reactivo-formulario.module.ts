import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReactivoFormularioComponent } from './reactivo-formulario.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { LucideAngularModule, X } from 'lucide-angular';

@NgModule({
  declarations: [ReactivoFormularioComponent],
  imports: [
    CommonModule,
    FormsModule,
    NgSelectModule,
    BsDatepickerModule,
    LucideAngularModule.pick({X}),
  ],
  exports: [],
  providers: [],
})
export class ReactivoFormularioModule {}
