import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { EstadoSelectorDto } from '@dtos/catalogo/estado-selector-dto';
import { MunicipioSelectorDto } from '@dtos/catalogo/municipio-selector-dto';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import { CompaniaLogotipoService } from '@http/catalogo/compania-logotipo.service';
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
import { AccesoService } from '@http/seguridad/acceso.service';
import { Compania } from '@models/catalogo/compania';
import { CompaniaContacto } from '@models/catalogo/compania-contacto';
import { CompaniaLogotipo } from '@models/catalogo/compania-logotipo';
import { GiroComercial } from '@models/catalogo/giro-comercial';
import { Lada } from '@models/catalogo/lada';
import { Moneda } from '@models/catalogo/moneda';
import { Pais } from '@models/catalogo/pais';
import { RegimenFiscal } from '@models/catalogo/regimen-fiscal';
import { TipoCompania } from '@models/catalogo/tipo-compania';
import { AgrupadorCuentaContable } from '@models/contabilidad/agrupador-cuenta-contable';
import { Usuario } from '@models/seguridad/usuario';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { ACCESO_COMPANIA } from 'src/app/shared/utils/codigos-acceso/catalogo.accesos';
import { GeneralConstant } from 'src/app/shared/utils/general-constant';
import * as Utileria from 'src/app/shared/utils/utileria';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-compania-informacion-formulario',
  templateUrl: './compania-informacion-formulario.component.html',
  styleUrls: ['./compania-informacion-formulario.component.scss']
})
export class CompaniaInformacionFormularioComponent implements OnInit {
  @ViewChild('companiaLogotipoInput', { static: false })
  public companiaLogotipoInput: ElementRef;

  // Se utilza cuando el componente se llama sin utilizar el componente padre (compania-formulario)
  @Input()
  public titulo: string = 'Agregar Compañía';

  @Input()
  public idCompania: number;

  @Input()
  public accion: string;

  // Se asume que el componente se llama desde el login. En caso de que se mande a llamar desde el componente
  // padre (compania-formulario) se debe mandar a llamar con false
  @Input()
  public desdeLogin: boolean = true;

  public tieneAccesoEditar = false;
  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;
  public mensajeEditar = 'La compañía ha sido modificada';
  public btnSubmit = false;
  public compania: Compania = new Compania();
  public companiaContacto: CompaniaContacto = new CompaniaContacto();

  public paisList: Pais[] = [];
  public estadoList: EstadoSelectorDto[] = [];
  public ciudadList: MunicipioSelectorDto[] = [];
  public ladaList: Lada[] = [];
  public regimenFiscalList: RegimenFiscal[] = [];
  public agrupadorCuentaList: AgrupadorCuentaContable[] = [];
  public tipoCompaniaList: TipoCompania[] = [];
  public giroComercialList: GiroComercial[] = [];
  public monedaList: Moneda[] = [];

  public urlFrontend: string;
  public readonly urlBackend: string = environment.urlBackend;
  public usuario = new Usuario();
  public confirmarContrasena: string = '';

  public companiaLogotipo: CompaniaLogotipo | null = new CompaniaLogotipo();

  public configLada = Object.assign(
    { labelField: 'claveNumero', valueField: 'idLada', searchField: ['claveNumero'], dropdownParent: 'body' },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  public configRegimenFiscal = Object.assign(
    { labelField: 'claveNombre', valueField: 'idRegimenFiscal', searchField: ['claveNombre'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  public configAgrupadorCuenta = Object.assign(
    { labelField: 'descripcion', valueField: 'idAgrupadorCuentaContable', searchField: ['descripcion'], dropdownParent: 'body' },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  public configTipoCompania = Object.assign(
    { labelField: 'nombre', valueField: 'idTipoCompania', searchField: ['nombre'], dropdownParent: 'body' },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  public configMoneda = Object.assign(
    { labelField: 'claveNombre', valueField: 'idMoneda', searchField: ['claveNombre'], dropdownParent: 'body' },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  public configGiroComercial = Object.assign(
    { labelField: 'nombre', valueField: 'idGiroComercial', searchField: ['nombre'], dropdownParent: 'body' },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  constructor(
    private accesoService: AccesoService,
    private agrupadorCuentaContableService: AgrupadorCuentaContableService,
    private codigoPostalService: CodigoPostalService,
    private companiaService: CompaniaService,
    private estadoService: EstadoService,
    private giroComercialService: GiroComercialService,
    private ladaService: LadaService,
    private modalMensajeService: MensajeService,
    private monedaService: MonedaService,
    private municipioService: MunicipioService,
    private paisService: PaisService,
    private regimenFiscalService: RegimenFiscalService,
    private router: Router,
    private tipoCompaniaService: TipoCompaniaService,
    private companiaLogotipoService: CompaniaLogotipoService
  ) { }

  async ngOnInit() {
    if (this.desdeLogin) {
      // Si se manda a llamar desde el login es porque searchField agregará una nueva compañía
      this.accion = GeneralConstant.COMPONENT_ACCION_AGREGAR;
      this.tieneAccesoEditar = true;
    }
    else {
      this.accesoService
        .tieneAcceso(ACCESO_COMPANIA.Editar)
        .toPromise()
        .then((tieneAcceso) => this.tieneAccesoEditar = tieneAcceso ?? false)
        .catch(() => {});
    }

    if (this.accion === GeneralConstant.COMPONENT_ACCION_EDITAR) {
      this.consultarLogotipo(this.idCompania);
      await this.consultarCompania(this.idCompania);
    }

    this.urlFrontend = Utileria.obtenerUrlComprasEnLineaFrontend();

    await Promise.all([
      this.consultarPaises(),
      this.consultarLadas(),
      this.consultarRegimenFiscal(),
      this.consultarAgrupadores(),
      this.consultarTiposCompania(),
      this.consultarMonedas(),
      this.consultarGirosComerciales()
    ]);
  }

  private async consultarCompania(idCompania: number) {
    const compania = await this.companiaService
      .consultar(idCompania)
      .toPromise();

    this.compania = compania ?? new Compania();
  }

  private async consultarLogotipo(idCompania: number): Promise<void> {
    const companiaLogotipo = await this.companiaLogotipoService
      .consultarPorCompania(idCompania)
      .toPromise()
      .catch((error) => console.log(error));

    this.companiaLogotipo = companiaLogotipo ?? null;
  }

  private async consultarAgrupadores(): Promise<void> {
    const agrupadoresCuentaContable = await this.agrupadorCuentaContableService.consultarParaSelector()
      .toPromise()
      .catch(() => []);

    this.agrupadorCuentaList = agrupadoresCuentaContable ?? [];
  }

  private async consultarTiposCompania(): Promise<void> {
    const tiposCompania = await this.tipoCompaniaService.consultarParaSelector()
      .toPromise()
      .catch(() => []);

    this.tipoCompaniaList = tiposCompania ?? [];
  }

  private async consultarLadas(): Promise<void> {
    const ladas = await this.ladaService.consultarTodosParaSelector()
      .toPromise()
      .catch(() => []);

    this.ladaList = ladas ?? [];
  }

  private async consultarRegimenFiscal(): Promise<void> {
    const regimenesFiscales = await this.regimenFiscalService.consultarTodosParaSelector()
      .toPromise()
      .then((regimenesFiscales) => {
        regimenesFiscales?.forEach(rf => rf.claveNombre = `${rf.clave} - ${rf.nombre}`);
        return regimenesFiscales;
      })
      .catch(() => []);

    this.regimenFiscalList = regimenesFiscales ?? [];
  }

  private async consultarMonedas(): Promise<void> {
    const monedas = await this.monedaService.consultarParaSelector()
      .toPromise()
      .then((monedas) => {
        monedas?.forEach((m) => m.claveNombre = `${m.nombre} (${m.clave})`);
        return monedas;
      })
      .catch(() => []);

    this.monedaList = monedas ?? [];
  }

  private async consultarPaises(): Promise<void> {
    const paises = await this.paisService.consultarTodosParaSelector()
      .toPromise()
      .catch(() => []);

    if (!paises || paises.length === 0) {
      this.paisList = [];
      return;
    }

    this.paisList = paises ?? [];

    if (!this.compania.idPais) {
      const paisMexico = this.paisList.find((p) => p.clave === GeneralConstant.CLAVE_PAIS_MEXICO);
      this.compania.idPais = paisMexico?.idPais ?? 0;
    }

    this.consultarEstados(this.compania.idPais);
  }

  private async consultarEstados(idPais: number): Promise<void> {
    const estados = await this.estadoService.consultarPorPaisParaSelector(idPais)
      .toPromise()
      .catch(() => []);

    this.estadoList = estados ?? [];

    this.consultarMunicipios(this.compania.idEstado);
  }

  private async consultarMunicipios(idEstado: number): Promise<void> {
    const ciudades = idEstado > 0
      ? await this.municipioService.consultarPorEstadoParaSelector(idEstado).toPromise()
      : [];

    this.ciudadList = ciudades ?? [];
  }

  private async consultarGirosComerciales(): Promise<void> {
    const girosComerciales = await this.giroComercialService.consultarTodos()
      .toPromise()
      .catch(() => []);

    this.giroComercialList = girosComerciales ?? [];
  }

  public onPaisChange(idPais: number): void {
    this.compania.idEstado = 0;
    this.estadoList = [];

    if (idPais > 0) {
      this.consultarEstados(idPais);
    }
  }

  public async onCodigoPostalChange(codigoPostal: string): Promise<void> {
    if (codigoPostal.length != 5) {
      return;
    }

    const codigos = await this.codigoPostalService.consultarPorCodigoPostal(codigoPostal)
      .toPromise()
      .catch(() => []);

    if (!codigos || codigos.length === 0) {
      return;
    }

    this.consultarMunicipios(codigos[0].idEstado);

    this.compania.idEstado = codigos[0].idEstado;
    this.compania.idMunicipio = codigos[0].idMunicipio;
  }

  public onFileChangeLogotipo(event: any): void {
    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]);

      this.companiaLogotipo = new CompaniaLogotipo();
      this.companiaLogotipo.nombreImagen = event.target.files[0].name;
      this.companiaLogotipo.tipoMime = event.target.files[0].type;

      reader.onload = (event: Event) => {
        const result = reader.result;

        if (!result) {
          return;
        }

        this.companiaLogotipo!.imagenBase64 = result.toString().split(',')[1];
      };
    }
  }

  public eliminarLogotipo(): void {
    if (!this.companiaLogotipo || !this.companiaLogotipo.idCompaniaLogotipo) {
      this.companiaLogotipo = null;
      this.companiaLogotipoInput.nativeElement.value = '';
      return;
    }

    this.modalMensajeService
      .modalConfirmacion(
        '¿Desea eliminar el logotipo de la compañía?',
        'Eliminar Logotipo de Compañía',
        GeneralConstant.ICONO_CRUZ
      )
      .then(() => {
        this.companiaLogotipoService
          .eliminar(this.companiaLogotipo!.idCompaniaLogotipo)
          .subscribe(() => {
            this.companiaLogotipo = null;
            this.companiaLogotipoInput.nativeElement.value = '';
            this.modalMensajeService.modalExito('El logotipo ha sido eliminado');
          });
      })
      .catch(() => {});
  }

  public obtenerSrcLogotipo(companiaLogotipo: CompaniaLogotipo): string {
    return `data:${companiaLogotipo.tipoMime};base64,${companiaLogotipo.imagenBase64}`;
  }

  private async agregar(): Promise<void> {
    const idCompania = await this.companiaService
      .agregar(this.compania)
      .toPromise();

    if (!idCompania) {
      return;
    }

    if (this.companiaLogotipo !== null && this.companiaLogotipo.imagenBase64 != null) {
      this.companiaLogotipo.idCompania = idCompania;

      await this.companiaLogotipoService
        .agregar(this.companiaLogotipo)
        .toPromise();
    }
  }

  private async editar(): Promise<void> {
    await this.companiaService
      .editar(this.compania)
      .toPromise();

    if (this.companiaLogotipo !== null && this.companiaLogotipo.imagenBase64 != null) {
      this.companiaLogotipo.idCompania = this.compania.idCompania;

      await this.companiaLogotipoService
        .agregar(this.companiaLogotipo)
        .toPromise();
    }
  }

  public async enviarFormulario(formulario: NgForm): Promise<void> {
    this.btnSubmit = true;

    if (!formulario.valid) {
      Utileria.validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      return;
    }

    if (this.desdeLogin) {
      this.compania.companiaContacto = this.companiaContacto;
    }

    const acciones: { [key: string]: [Function, string] } = {
      [GeneralConstant.COMPONENT_ACCION_EDITAR]: [this.editar.bind(this), 'La compañía ha sido editada'],
      [GeneralConstant.COMPONENT_ACCION_AGREGAR]: [this.agregar.bind(this), 'La compañía ha sido agregada']
    };

    const [funcion, mensaje] = acciones[this.accion];

    try {
      await funcion();
    }
    catch(error) {
      this.btnSubmit = false;
      return;
    }

    this.modalMensajeService.modalExito(mensaje);
    this.regresar();
  }

  public regresar() {
    if (this.desdeLogin) {
      this.router.navigate(['login']);
    }
    else {
      this.router.navigate(['administrador/configuracion-general/catalogo/compania']);
    }
  }

  public esEditar(): boolean {
    return this.accion === GeneralConstant.COMPONENT_ACCION_EDITAR;
  }
}
