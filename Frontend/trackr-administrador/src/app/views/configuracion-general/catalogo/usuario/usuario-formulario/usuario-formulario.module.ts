import { NgModule } from "@angular/core";
import { CodigoPostalService } from "@http/catalogo/codigo-postal.service";
import { ColoniaService } from "@http/catalogo/colonia.service";
import { CompaniaService } from "@http/catalogo/compania.service";
import { CuentaContableService } from "@http/catalogo/cuenta-contable.service";
import { EstadoService } from "@http/catalogo/estado.service";
import { HospitalService } from "@http/catalogo/hospital.service";
import { ListaPrecioService } from "@http/catalogo/lista-precio.service";
import { LocalidadService } from "@http/catalogo/localidad.service";
import { MunicipioService } from "@http/catalogo/municipio.service";
import { PaisService } from "@http/catalogo/pais.service";
import { PuntoVentaService } from "@http/catalogo/punto-venta.service";
import { RegimenFiscalService } from "@http/catalogo/regimen-fiscal.service";
import { TipoClienteService } from "@http/catalogo/tipo-cliente.service";
import { TituloAcademicoService } from "@http/catalogo/titulo-academico.service";
import { SatFormaPagoService } from "@http/facturacion/sat-forma-pago.service";
import { MetodoPagoService } from "@http/gestion-caja/metodo-pago.service";
import { AccesoService } from "@http/seguridad/acceso.service";
import { PerfilService } from "@http/seguridad/perfil.service";
import { RolService } from "@http/seguridad/rol.service";
import { TipoUsuarioService } from "@http/seguridad/tipo-usuario.service";
import { UsuarioLocacionService } from "@http/seguridad/usuario-locacion.service";
import { UsuarioRolService } from "@http/seguridad/usuario-rol.service";
import { UsuarioService } from "@http/seguridad/usuario.service";
import { NgSelectModule } from "@ng-select/ng-select";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { BsDatepickerModule } from "ngx-bootstrap/datepicker";
import { ModalModule } from "ngx-bootstrap/modal";
import { TabsModule } from "ngx-bootstrap/tabs";
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { DirectiveModule } from "src/app/shared/directives/directive.module";
import { SharedModule } from "src/app/shared/shared.module";
import { UsuarioFormularioComponent } from "./usuario-formulario.component";
import { GridGeneralModule } from "@sharedComponents/grid-general/grid-general.module";
import { Camera, LucideAngularModule, Trash, Trash2, X } from "lucide-angular";
import { MatTabsModule } from "@angular/material/tabs";

@NgModule({
  imports: [
    SharedModule,
    DirectiveModule,
    BsDatepickerModule,
    CollapseModule,
    TabsModule,
    ModalModule.forChild(),
    NgSelectModule,
    TypeaheadModule.forRoot(),
    GridGeneralModule,
    LucideAngularModule.pick({ X , Camera , Trash2 }),
    MatTabsModule
  ],
  declarations: [UsuarioFormularioComponent],
  exports: [UsuarioFormularioComponent],
  providers: [
    AccesoService,
    CodigoPostalService,
    ColoniaService,
    CompaniaService,
    CuentaContableService,
    EstadoService,
    HospitalService,
    LocalidadService,
    MetodoPagoService,
    MunicipioService,
    PaisService,
    PerfilService,
    PuntoVentaService,
    RegimenFiscalService,
    RolService,
    TipoUsuarioService,
    TituloAcademicoService,
    UsuarioLocacionService,
    UsuarioRolService,
    UsuarioService,
    SatFormaPagoService,
    ListaPrecioService,
    TipoClienteService
  ],
  entryComponents: [UsuarioFormularioComponent]
})
export class UsuarioFormularioModule {}
