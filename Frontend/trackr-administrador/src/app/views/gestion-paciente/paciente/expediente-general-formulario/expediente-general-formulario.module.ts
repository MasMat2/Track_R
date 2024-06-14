import { SharedModule } from 'src/app/shared/shared.module';
import { ExpedienteGeneralFormularioComponent } from './expediente-general-formulario.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BusquedaExpedienteModule } from '../busqueda-expediente/busqueda-expediente.module';
import { BusquedaExpedienteComponent } from '../busqueda-expediente/busqueda-expediente.component';
import { UsuarioFormularioModule } from 'src/app/views/configuracion-general/catalogo/usuario/usuario-formulario/usuario-formulario.module';
import { EntidadEstructuraService } from '@http/gestion-entidad/entidad-estructura.service';
import { MatButtonModule } from '@angular/material/button';
import { Calendar, LucideAngularModule, Plus } from 'lucide-angular';

@NgModule({
    declarations: [ExpedienteGeneralFormularioComponent],
    exports: [ExpedienteGeneralFormularioComponent],
    entryComponents: [
        ExpedienteGeneralFormularioComponent,
        BusquedaExpedienteComponent
    ],
    providers: [
        EntidadEstructuraService
    ],
    imports: [
        CommonModule,
        FormsModule,
        SharedModule,
        BsDatepickerModule,
        ReactiveFormsModule,
        BusquedaExpedienteModule,
        UsuarioFormularioModule,
        MatButtonModule,
        LucideAngularModule.pick({Plus, Calendar})
    ]
})
export class ExpedienteGeneralFormularioModule {}