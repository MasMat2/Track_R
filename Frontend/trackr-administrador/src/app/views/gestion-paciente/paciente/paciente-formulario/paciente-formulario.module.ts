import { SharedModule } from 'src/app/shared/shared.module';
import { PacienteFormularioComponent } from './paciente-formulario.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DomicilioFormularioModule } from "../../../../shared/components/domicilio-formulario/domicilio-formulario.module";
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
    declarations: [PacienteFormularioComponent],
    exports: [PacienteFormularioComponent],
    entryComponents: [PacienteFormularioComponent],
    providers: [],
    imports: [
        CommonModule,
        FormsModule,
        SharedModule,
        BsDatepickerModule
    ]
})
export class PacienteFormularioModule {}