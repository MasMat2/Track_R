import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import { CompaniaService } from '@http/catalogo/compania.service';
import { EstadoService } from '@http/catalogo/estado.service';
import { GiroComercialService } from '@http/catalogo/giro-comercial.service';
import { LadaService } from '@http/catalogo/lada.service';
import { MonedaService } from '@http/catalogo/moneda.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { RegimenFiscalService } from '@http/catalogo/regimen-fiscal.service';
import { TipoCompaniaService } from '@http/catalogo/tipo-compania.service';
import { AgrupadorCuentaContableService } from '@http/contabilidad/agrupador-cuenta-contable.service';
import { NgSelectModule } from '@ng-select/ng-select';
import { ModalModule } from 'ngx-bootstrap/modal';
import { SharedModule } from 'src/app/shared/shared.module';
import { CompaniaInformacionFormularioComponent } from './compania-informacion-formulario.component';
import { CompaniaInformacionFormularioRoutingModule } from './compania-informacion-formulario.routing.module';
import { FormsModule } from '@angular/forms';

@NgModule({
	imports: [
		// SharedModule,
    CommonModule,
    FormsModule,
		// DirectiveModule,
		CommonModule,
		ModalModule.forChild(),
		NgSelectModule,
		CompaniaInformacionFormularioRoutingModule,
    NgSelectModule
	],
	exports: [CompaniaInformacionFormularioComponent],
	declarations: [CompaniaInformacionFormularioComponent],
	providers: [
		EstadoService,
		PaisService,
		CompaniaService,
		RegimenFiscalService,
		LadaService,
		MunicipioService,
		CodigoPostalService,
		AgrupadorCuentaContableService,
		TipoCompaniaService,
		MunicipioService,
		MonedaService,
		GiroComercialService,
	],
})
export class CompaniaInformacionFormularioModule {}
