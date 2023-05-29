import { SharedModule } from 'src/app/shared/shared.module';
import { ExpedienteFormularioComponent as ExpedienteFormularioComponent } from './expediente-formulario.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
    declarations: [ExpedienteFormularioComponent],
    exports: [ExpedienteFormularioComponent],
    entryComponents: [ExpedienteFormularioComponent],
    providers: [],
    imports: [
        CommonModule,
        FormsModule,
        SharedModule,
        BsDatepickerModule
    ]
})
export class ExpedienteFormularioModule {}