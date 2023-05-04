import { NgModule } from "@angular/core";
import { CompaniaService } from "@http/catalogo/compania.service";
import { EstadoService } from "@http/catalogo/estado.service";
import { MunicipioService } from "@http/catalogo/municipio.service";
import { PaisService } from "@http/catalogo/pais.service";
import { RegimenFiscalService } from "@http/catalogo/regimen-fiscal.service";
import { PerfilService } from "@http/seguridad/perfil.service";
import { NgSelectModule } from "@ng-select/ng-select";
import { GridFiltroModule } from "@sharedComponents/grid-filtro/grid-filtro.module";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { ModalModule } from "ngx-bootstrap/modal";
import { TabsModule } from "ngx-bootstrap/tabs";
import { DirectiveModule } from "src/app/shared/directives/directive.module";
import { SharedModule } from "src/app/shared/shared.module";
import { UsuarioFormularioComponent } from "./usuario-formulario/usuario-formulario.component";
import { UsuarioFormularioModule } from "./usuario-formulario/usuario-formulario.module";
import { UsuarioComponent } from "./usuario.component";
import { UsuarioRoutingModule } from "./usuario.routing.module";
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { RolService } from "@http/seguridad/rol.service";
import { TipoUsuarioService } from "@http/seguridad/tipo-usuario.service";
import { UsuarioService } from "@http/seguridad/usuario.service";
import { ColoniaService } from "@http/catalogo/colonia.service";
import { CuentaContableService } from "@http/catalogo/cuenta-contable.service";
import { HospitalService } from "@http/catalogo/hospital.service";
import { ListaPrecioService } from "@http/catalogo/lista-precio.service";
import { LocalidadService } from "@http/catalogo/localidad.service";
import { PuntoVentaService } from "@http/catalogo/punto-venta.service";
import { TipoClienteService } from "@http/catalogo/tipo-cliente.service";
import { TituloAcademicoService } from "@http/catalogo/titulo-academico.service";
import { UsuarioLocacionService } from "@http/seguridad/usuario-locacion.service";
import { UsuarioRolService } from "@http/seguridad/usuario-rol.service";

@NgModule({
  imports: [
    SharedModule,
    UsuarioRoutingModule,
    DirectiveModule,
    // Daterangepicker,
    BsDatepickerModule,
    CollapseModule,
    TabsModule,
    ModalModule.forChild(),
    NgSelectModule,
    UsuarioFormularioModule,
    GridFiltroModule
  ],
  declarations: [UsuarioComponent],
  entryComponents: [UsuarioFormularioComponent],
  providers: [
    UsuarioService,
    CompaniaService,
    HospitalService,
    PerfilService,
    TituloAcademicoService,
    PaisService,
    EstadoService,
    MunicipioService,
    LocalidadService,
    ColoniaService,
    RolService,
    UsuarioRolService,
    PuntoVentaService,
    TipoUsuarioService,
    UsuarioLocacionService,
    CuentaContableService,
    RegimenFiscalService,
    CuentaContableService,
    TipoClienteService,
    ListaPrecioService
  ],
})
export class UsuarioModule {}
