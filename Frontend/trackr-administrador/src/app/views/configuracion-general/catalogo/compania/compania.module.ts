import { NgModule } from '@angular/core';
import { CompaniaComponent } from './compania.component';
import { GridGeneralModule } from 'src/app/shared/components/grid-general/grid-general.module';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { CompaniaRoutingModule } from './compania.routing.module';
import { CompaniaService } from '@http/catalogo/compania.service';
import { TipoCompaniaService } from '@http/catalogo/tipo-compania.service';
import { CommonModule } from '@angular/common';
import { CompaniaInformacionFormularioModule } from './compania-formulario/compania-informacion-formulario/compania-informacion-formulario.module';

@NgModule({
  declarations: [
    CompaniaComponent
  ],
  imports: [
    CommonModule,
    GridGeneralModule,
    CollapseModule,
    FormsModule,
    NgSelectModule,
    CompaniaRoutingModule,
    CompaniaInformacionFormularioModule
  ],
  exports: [
    CompaniaComponent
  ],
  providers: [
    CompaniaService,
    TipoCompaniaService
  ]
})
export class CompaniaModule { }
