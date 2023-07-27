import { NgModule } from '@angular/core';
import { BancoService } from '@http/catalogo/banco.service';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import { CompaniaService } from '@http/catalogo/compania.service';
import { EstadoService } from '@http/catalogo/estado.service';
import { HospitalLogotipoService } from '@http/catalogo/hospital-logotipo.service';
import { HospitalService } from '@http/catalogo/hospital.service';
import { LadaService } from '@http/catalogo/lada.service';
import { ListaPrecioService } from '@http/catalogo/lista-precio.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { RegimenFiscalService } from '@http/catalogo/regimen-fiscal.service';
import { AlmacenService } from '@http/inventario/almacen.service';
import { CertificadoLocacionService } from '@http/seguridad/certificado-locacion.service';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { CertificadoConfiguracionComponent } from './certificado-configuracion/certificado-configuracion.component';
import { HospitalFormularioComponent } from './locacion-formulario/locacion-formulario.component';
import { HospitalComponent } from './locacion.component';
import { HospitalRoutingModule } from './locacion.routing.module';

@NgModule({
  declarations: [
    HospitalComponent,
    HospitalFormularioComponent,
    CertificadoConfiguracionComponent
  ],
  imports: [
    HospitalRoutingModule,
    SharedModule,
    DirectiveModule,
    ModalModule.forChild(),
    TypeaheadModule.forRoot()
  ],
  providers: [
    HospitalService,
    EstadoService,
    PaisService,
    BancoService,
    UsuarioService,
    CompaniaService,
    ListaPrecioService,
    RegimenFiscalService,
    LadaService,
    HospitalLogotipoService,
    MunicipioService,
    CodigoPostalService,
    AlmacenService,
    CertificadoLocacionService
  ],
  entryComponents: [
    CertificadoConfiguracionComponent
  ]
})
export class HospitalModule { }
