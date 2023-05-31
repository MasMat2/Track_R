import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BusquedaExpedienteComponent } from './busqueda-expediente.component';
import { PopoverModule } from '@coreui/angular';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@NgModule({
  imports: [
    CommonModule,
    PopoverModule,
    DirectiveModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    PaginationModule.forRoot()
  ],
  declarations: [BusquedaExpedienteComponent],
})
export class BusquedaExpedienteModule { }
