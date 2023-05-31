import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DomicilioFormularioComponent } from './domicilio-formulario.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTabsModule } from '@angular/material/tabs';
import { NgSelectModule } from '@ng-select/ng-select';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { CollapseModule, SharedModule, TabsModule } from '@coreui/angular';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { DirectiveModule } from '../../directives/directive.module';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import { ColoniaService } from '@http/catalogo/colonia.service';
import { EstadoService } from '@http/catalogo/estado.service';
import { LocalidadService } from '@http/catalogo/localidad.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';

@NgModule({
  imports: [
    CommonModule,
    NgSelectModule,
    FormsModule,
    GridGeneralModule,
    MatTabsModule,
    SharedModule,
    DirectiveModule,
    BsDatepickerModule,
    CollapseModule,
    TabsModule,
    NgSelectModule,
    TypeaheadModule.forRoot(),
  ],
  declarations: [DomicilioFormularioComponent],
  providers: [
    CodigoPostalService,
    ColoniaService,
    EstadoService, 
    LocalidadService, 
    MunicipioService,
    PaisService,
  ],
  exports: [DomicilioFormularioComponent],
})
export class DomicilioFormularioModule { }
