import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { EstadoSelectorDto } from '@dtos/catalogo/estado-selector-dto';
import { MunicipioSelectorDto } from '@dtos/catalogo/municipio-selector-dto';
import { AreaService } from '@http/catalogo/area.service';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import { ColoniaService } from '@http/catalogo/colonia.service';
import { CompaniaService } from '@http/catalogo/compania.service';
import { ConceptoService } from '@http/catalogo/concepto.service';
import { CuentaContableService } from '@http/catalogo/cuenta-contable.service';
import { EstadoService } from '@http/catalogo/estado.service';
import { HospitalService } from '@http/catalogo/hospital.service';
import { ListaPrecioService } from '@http/catalogo/lista-precio.service';
import { LocalidadService } from '@http/catalogo/localidad.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { PuntoVentaService } from '@http/catalogo/punto-venta.service';
import { RegimenFiscalService } from '@http/catalogo/regimen-fiscal.service';
import { TipoClienteService } from '@http/catalogo/tipo-cliente.service';
import { TituloAcademicoService } from '@http/catalogo/titulo-academico.service';
import { SatFormaPagoService } from '@http/facturacion/sat-forma-pago.service';
import { MetodoPagoService } from '@http/gestion-caja/metodo-pago.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { PerfilService } from '@http/seguridad/perfil.service';
import { RolService } from '@http/seguridad/rol.service';
import { TipoUsuarioService } from '@http/seguridad/tipo-usuario.service';
import { UsuarioLocacionService } from '@http/seguridad/usuario-locacion.service';
import { UsuarioRolService } from '@http/seguridad/usuario-rol.service';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { Area } from '@models/catalogo/area';
import { CodigoPostal } from '@models/catalogo/codigo-postal';
import { Colonia } from '@models/catalogo/colonia';
import { Compania } from '@models/catalogo/compania';
import { Concepto } from '@models/catalogo/concepto';
import { CuentaContable } from '@models/catalogo/cuenta-contable';
import { Hospital } from '@models/catalogo/hospital';
import { ListaPrecio } from '@models/catalogo/lista-precio';
import { Localidad } from '@models/catalogo/localidad';
import { Pais } from '@models/catalogo/pais';
import { PuntoVenta } from '@models/catalogo/punto-venta';
import { RegimenFiscal } from '@models/catalogo/regimen-fiscal';
import { TipoCliente } from '@models/catalogo/tipo-cliente';
import { TituloAcademico } from '@models/catalogo/titulo-academico';
import { SatFormaPago } from '@models/facturacion/sat-forma-pago';
import { MetodoPago } from '@models/gestion-caja/metodo-pago';
import { Perfil } from '@models/seguridad/perfil';
import { Rol } from '@models/seguridad/rol';
import { TipoUsuario } from '@models/seguridad/tipo-usuario';
import { Usuario } from '@models/seguridad/usuario';
import { UsuarioLocacion } from '@models/seguridad/usuario-locacion';
import { UsuarioRol } from '@models/seguridad/usuario-rol';
import { UsuarioImagenService } from '@services/usuario-imagen.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import * as Utileria from '@utils/utileria';
import { Observable, Observer, of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import { ArchivoService } from './../../../../../shared/http/catalogo/archivo.service';
import { AlertifyService } from '@services/alertify.service';
import { EspecialidadService } from '@http/catalogo/especialidad.service';
import { Especialidad } from '@models/catalogo/especialidad';
import { EspecialidadGridDto } from '@dtos/catalogo/especialidad-grid-dto';

/**
 * Formulario de usuario, permite agregar, editar y eliminar.
 */
@Component({
  selector: 'app-usuario-formulario',
  templateUrl: './usuario-formulario.component.html',
  styleUrls: ['./usuario-formulario.component.scss']
})
export class UsuarioFormularioComponent implements OnInit {
  @ViewChild('formulario', { static: false }) public formulario: NgForm;
  @ViewChild('codigoPostal', { static: false }) public codigoPostal: NgModel;

  public titulo = 'Agregar';
  public accion = 'Agregar';
  public desdeExpediente = false;
  public onClose: any;

  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;
  public configDate = GeneralConstant.CONFIG_DATEPICKER;

  public MENSAJE_AGREGAR = 'El usuario ha sido agregado';
  public MENSAJE_EDITAR = 'El usuario ha sido modificado';
  public MENSAJE_PASSWORD = 'Las contraseñas no coinciden';

  public usuario = new Usuario();

  // Información General
  public tituloList: TituloAcademico[];
  public perfilList: Perfil[];
  public tipoUsuarioList: TipoUsuario[];
  public rolList: Rol[];
  public companiaList: Compania[];
  public confirmarContrasena: string = '';
  public puntoVentaList: PuntoVenta[];
  public areaList: Area[];
  public regimenFiscalList: RegimenFiscal[];
  public especialidadList : EspecialidadGridDto[];

  public rolSeleccionados: number[] = [];
  public especialidadSeleccionadas: number[] = [];

  public tieneRolVendedor: boolean = false;
  public tieneRolMedico: boolean = false;
  public tieneRolCliente: boolean = false;

  public esExterno: boolean = false;
  public rfcValido: boolean = false;
  public accesoContrasena: boolean = false;

  // Datos de Contacto
  public paisList: Pais[] = [];
  public estadoList: EstadoSelectorDto[] = [];
  public municipioList: MunicipioSelectorDto[] = [];
  public localidadList: Localidad[] = [];
  public coloniaList: Colonia[] = [];

  public sugerenciasCodigoPostal$: Observable<CodigoPostal[]>;

  public esPaisExtranjero: boolean = false;
  public idPaisMexico: number;

  // Datos de Socio Comercial
  public tipoClienteList: TipoCliente[];
  public listaPrecioList: ListaPrecio[];

  // Datos de Pago
  public metodoPagoCredito?: MetodoPago;
  public metodoPagoList: MetodoPago[] = [];
  public formaPagoList: SatFormaPago[] = [];

  // Imagen
  public imagenBase64: any;
  public url: any;
  public urlName: string;
  public urlImagenDefault = './assets/img/svg/ico-36x36-header-usuario.svg';

  // Permisos
  public hospitalList: Hospital[];
  public btnSubmit = false;
  public esEdicion = false;

  // Permisos
  public usuarioLocacion = new UsuarioLocacion();
  public permisosList: UsuarioLocacion[];

  public columns = [
    { headerName: 'Compañía', field: 'compania', minWidth: 100, },
    { headerName: 'Hospital', field: 'locacion', minWidth: 100 },
    { headerName: 'Perfil', field: 'perfil', minWidth: 100, },
  ];

  // Afectación contable ROL - CUENTA CONTABLE
  public usuarioRol = new UsuarioRol();
  public usuarioRolList: UsuarioRol[] = [];
  public conceptoList: Concepto[] = [];
  public cuentaContableList: CuentaContable[] = [];

  public columnsUsuarioRol = [
    { headerName: 'Rol', field: 'nombreRol', minWidth: 100, },
    { headerName: 'Concepto', field: 'concepto', minWidth: 100, },
    { headerName: 'Cuenta Contable', field: 'cuentaContable', minWidth: 100, },
  ];

  public configIdRol = Object.assign(
    {
      labelField: 'nombre',
      valueField: 'idRol',
      searchField: ['nombre'],
      plugins: ['dropdown_direction', 'remove_button'],
      maxItems: null,
      dropdownDirection: 'down',
    }
  );

  public configModal: any = {
    animated: true,
    keyboard: false,
    backdrop: 'static',
    ignoreBackdropClick: true,
    size: 'sm',
    class: 'modal-md modal-size-md modal-position-center'
  };

  constructor(
    private accesoService: AccesoService,
    private areaService: AreaService,
    private coloniaService: ColoniaService,
    private companiaService: CompaniaService,
    private conceptoService: ConceptoService,
    private cuentaContableService: CuentaContableService,
    private estadoService: EstadoService,
    private hospitalService: HospitalService,
    private localidadService: LocalidadService,
    private metodoPagoService: MetodoPagoService,
    private modalMensajeService: MensajeService,
    private municipioService: MunicipioService,
    private paisService: PaisService,
    private perfilService: PerfilService,
    private puntoVentaService: PuntoVentaService,
    private regimenFiscalService: RegimenFiscalService,
    private rolService: RolService,
    private sanitizer: DomSanitizer,
    private tipoUsuarioService: TipoUsuarioService,
    private tituloAcademicoService: TituloAcademicoService,
    private usuarioLocacionService: UsuarioLocacionService,
    private usuarioRolService: UsuarioRolService,
    private usuarioService: UsuarioService,
    private listaPrecioService: ListaPrecioService,
    private tipoClienteService: TipoClienteService,
    private satFormaPagoService: SatFormaPagoService,
    private codigoPostalService: CodigoPostalService,
    private archivoService: ArchivoService,
    private usuarioImagenService: UsuarioImagenService,
    private alertifyService : AlertifyService,
    private especialidadService : EspecialidadService
  ) {}

  public ngOnInit(): void {
    if (this.usuario.idUsuario > 0) {
      this.titulo = 'Editar';
      this.accion = GeneralConstant.MODAL_ACCION_EDITAR;
      this.esEdicion = true;
      this.consultarPermisos(this.usuario.idUsuario);
      this.consultarUsuarioRolParaGrid(this.usuario.idUsuario);
      this.rfcValido = this.usuario.rfc != "" && this.usuario.rfc != null;
    } else {
      this.accion = GeneralConstant.MODAL_ACCION_AGREGAR;
      this.usuario.sueldoDiario = 0;
    }

    this.usuario.contrasena = '';

    this.consultarAccesoContrasena();
    this.consultarTipoUsuario();
    this.consultarCompania();
    this.consultarPerfiles();
    this.consultarTitulos();
    this.consultarRoles();
    this.consultarPaises();
    this.consultarEstados(this.usuario.idPais);
    this.consultarMunicipios(this.usuario.idEstado);
    this.consultarLocalidades(this.usuario.idEstado);
    this.consultarColonias(this.usuario.codigoPostal);
    this.consultarDepartamentos();
    this.consultarPuntosVenta();
    this.consultarTiposDeUsuario();
    this.consultarConceptos();
    this.consultarRegimenFiscal();
    this.consultarColonias(this.usuario.codigoPostal);
    this.consultarCuentas();
    this.consultarListasPrecio();
    this.consultarTiposCliente();
    this.consultarMetodosPago();
    this.consultarFormasPago();
    this.cargarSugerenciasCodigoPostal();
    this.consultarEspecialidades();
  }

  private consultarAccesoContrasena(): void {
    this.accesoService.tieneAcceso(CodigoAcceso.EDITAR_CONTRASENA_USUARIO).subscribe((tieneAcceso) => {
      this.accesoContrasena = tieneAcceso;
    });
  }

  private consultarTipoUsuario(): void {
    if ( !(this.usuario.idTipoUsuario > 0) ) {
      this.tipoUsuarioService.consultarTipoAdministrador().subscribe((data) => {
        this.usuario.idTipoUsuario = data.idTipoUsuario;
      });
    }
  }

  private consultarPerfiles(): void {
    this.perfilService.consultarPorCompania().subscribe((data) => {
      this.perfilList = data;
    });
  }

  private consultarEspecialidades(): void {
    this.especialidadService.consultarParaGrid().subscribe((data) => {
      this.especialidadList = data;
      this.especialidadSeleccionadas = this.especialidadList.filter((especialidad) => 
        this.usuario.idsEspecialidad.includes(especialidad.idEspecialidad)).map((especialidad) => especialidad.idEspecialidad);
    });
  }

  private consultarPerfilesPorCompania(idCompania: number): void {
    if (idCompania > 0) {
      this.perfilService.consultarPorCompaniaParaSelector(idCompania).subscribe((data) => {
        this.perfilList = data;
      });
    }
  }

  private consultarTitulos(): void {
    this.tituloAcademicoService.consultarTodosParaSelector().subscribe((data) => {
      this.tituloList = data;
    });
  }

  private consultarHospitales(idCompania: number): void {
    if (idCompania > 0) {
      this.hospitalService.consultarPorCompania(idCompania).subscribe((data) => {
        this.hospitalList = data;
      });
    }
  }

  private consultarDepartamentos(): void {
    this.areaService.consultarTodosParaSelector().subscribe((data) => {
      this.areaList = data;
    });
  }

  private async consultarPaises(): Promise<void> {
    const paises = await this.paisService
      .consultarTodosParaSelector()
      .toPromise();

    this.paisList = paises ?? [];
  }

  private async consultarEstados(idPais: number): Promise<void> {
    const estados = idPais > 0
      ? await this.estadoService.consultarPorPaisParaSelector(idPais).toPromise()
      : [];

    this.estadoList = estados ?? [];
  }

  private async consultarMunicipios(idEstado: number): Promise<void> {
    const municipios = idEstado > 0
      ? await this.municipioService.consultarPorEstadoParaSelector(idEstado).toPromise()
      : [];

    this.municipioList = municipios ?? [];
  }

  private async consultarLocalidades(idEstado: number): Promise<void> {
    const localidades = idEstado > 0
      ? await this.localidadService.consultarPorEstado(idEstado).toPromise()
      : [];

    this.localidadList = localidades ?? [];
  }

  private async consultarColonias(codigoPostal: string): Promise<void> {
    const colonias = codigoPostal && codigoPostal.length === 5
      ? await this.coloniaService.consultarPorCodigoParaSelector(codigoPostal).toPromise()
      : [];

    this.coloniaList = colonias ?? [];
  }

  private consultarRoles(): void {
    this.rolService.consultarTodosParaSelector().subscribe((data) => {
      this.rolList = data;
    });
  }

  private consultarPuntosVenta(): void {
    this.puntoVentaService.consultarTodosParaSelector().subscribe((data) => {
      this.puntoVentaList = data;
    });
  }

  private consultarRegimenFiscal(): void {
    this.regimenFiscalService.consultarTodosParaSelector().subscribe((data) => {
      this.regimenFiscalList = data;
    });
  }

  private consultarCuentas(): void {
    this.cuentaContableService.consultarTodosParaSelector().subscribe((data) => {
      data.forEach(cuenta => {
        cuenta.nombre = cuenta.nombre + " - " + cuenta.numero;
      });
      this.cuentaContableList = data;
    });
  }

  public consultarTiposCliente(): void {
    this.tipoClienteService.consultarGeneral().subscribe((data) => {
      this.tipoClienteList = data;
    });
  }

  public consultarListasPrecio(): void {
    this.listaPrecioService.consultarTodosPorHospitalParaSelector().subscribe((data) => {
      this.listaPrecioList = data;
    });
  }

  private consultarMetodosPago(): void {
    this.metodoPagoService.consultarTodos().subscribe((metodosPago) => {
      this.metodoPagoList = metodosPago;
      this.metodoPagoCredito = metodosPago.find(metodo => metodo.clave === GeneralConstant.CLAVE_METODO_PAGO_CREDITO);
    });
  }

  private consultarFormasPago(): void {
    this.satFormaPagoService.consultarParaSelector().subscribe((formasPago) => {
      this.formaPagoList = formasPago;
    });
  }

  private consultarPaisMexico(): void {
    this.paisService.consultarPorClave(GeneralConstant.CLAVE_PAIS_MEXICO).subscribe((pais) => {
      this.idPaisMexico = pais.idPais;
    });
  }

  private cargarSugerenciasCodigoPostal(): void {
    this.sugerenciasCodigoPostal$ = new Observable(
      (observer: Observer<string>) => {
        observer.next(this.usuario.codigoPostal);
      }
    )
    .pipe(
      switchMap((query: string) => {
        if (!query || !this.usuario.idPais) {
          return of([]);
        }

        return this.codigoPostalService.consultarPorPaisBusqueda(query, this.usuario.idPais)
          .pipe(
            map((data) => data || []),
            catchError(() => of([]))
          );
      })
    );
  }

  public onCompaniaChange(value:number, esEdicion:boolean): void {
    this.hospitalList = [];
    this.perfilList = [];

    if (!esEdicion) {
      this.usuarioLocacion.idPerfil = 0;
      this.usuarioLocacion.idLocacion = 0;
    }

    if (value > 0) {
      this.consultarHospitales(value);
      this.consultarPerfilesPorCompania(value);
    }
  }

  private tieneRol(codigoRol: string): boolean {
    const roles = this.rolList.filter((rol) => { return this.rolSeleccionados.includes(rol.idRol) });

    return roles.some((rol) => rol.clave === codigoRol);
  }

  public onRfcChange(elementRef: NgModel): void {
    const errors = elementRef.control.errors;

    if (this.usuario.rfc == null || this.usuario.rfc == "" || (errors != null && errors['formatoRfc'])) {
      this.rfcValido = false;
    }
    else {
      this.rfcValido = true;
    }
  }

  public onRolChange(values: number[]): void {
    this.tieneRolVendedor = this.tieneRol(GeneralConstant.CLAVE_ROL_VENDEDOR);
    this.tieneRolMedico = this.tieneRol(GeneralConstant.CLAVE_ROL_MEDICO);
    this.tieneRolCliente = this.tieneRol(GeneralConstant.CLAVE_ROL_CLIENTE);
  }

  public async onChangePais(): Promise<void> {
    this.esPaisExtranjero = this.usuario.idPais !== this.idPaisMexico;
    this.usuario.idEstado = 0;
    await this.consultarEstados(this.usuario.idPais);

    this.onChangeEstado();
  }

  public async onChangeEstado(): Promise<void> {
    this.usuario.idMunicipio = 0;
    this.usuario.idLocalidad = 0;

    await Promise.all([
      this.consultarMunicipios(this.usuario.idEstado),
      this.consultarLocalidades(this.usuario.idEstado)
    ]);
  }

  public async onChangeCodigoPostal(): Promise<void> {
    this.usuario.idColonia = 0;

    const codigoPostalValue: string = this.usuario.codigoPostal;

    if (codigoPostalValue.length !== 5) {
      return;
    }

    await this.asignarValoresDeCodigoPostal(codigoPostalValue);
  }

  private async asignarValoresDeCodigoPostal(codigoPostalValue: string): Promise<void> {
    const codigoPostal = await this.codigoPostalService
      .consultarPorCodigoPostal(codigoPostalValue)
      .toPromise()
      .then((codigosPostales) => {
        return codigosPostales && codigosPostales.length > 0
          ? codigosPostales[0]
          : null;
      });

    if (!codigoPostal) {
      return;
    }

    this.usuario.idEstado = codigoPostal.idEstado;

    await Promise.all([
      this.consultarMunicipios(codigoPostal.idEstado),
      this.consultarLocalidades(codigoPostal.idEstado)
    ]);
    this.usuario.idMunicipio = codigoPostal.idMunicipio;
    this.usuario.idLocalidad = 0

    await this.consultarColonias(codigoPostalValue);
    const colonia = this.coloniaList
      .find((colonia) => Utileria.equalsNormalized(colonia.nombre, codigoPostal.colonia));

    if (colonia) {
      this.usuario.idColonia = colonia.idColonia;
    }
  }

  public onConceptoChange(idConcepto: any) {
    if (idConcepto) {
      let concepto = this.conceptoList.find(c => c.idConcepto == idConcepto);
      this.usuarioRol.idCuentaContable = concepto?.idCuentaContable ?? 0;
    }
  }

  public correoChange(value: string): void {
    var correoValido = Utileria.validarFormatoCorreo(value);
    if (correoValido) {
      this.usuario.correoPersonal = value;
    }
  }

  public consultarUsuario(): void {
    if (this.usuario.idUsuario > 0) {
      this.usuarioService.consultar(this.usuario.idUsuario).subscribe((data) => {
        this.usuario = data;

        if (this.usuario.idUsuario > 0) {
          this.consultarHospitales(this.usuario.idCompania);
        }

        this.consultarUsuarioRol();
        this.archivoService.obtenerUsuarioImagen(data.idUsuario).subscribe((imagen) => {
          let objectURL = URL.createObjectURL(imagen);
          this.url = this.sanitizer.bypassSecurityTrustUrl(objectURL);
          this.imagenBase64 = this.url;
        });
      });
    } else {
      this.toDataURL(this.urlImagenDefault, (myBase64: string) => {
        this.url = this.sanitize(myBase64);
      });
    }
  }

  public consultarUsuarioRol(): void {
    this.usuarioRolService.consultarPorUsuario(this.usuario.idUsuario).subscribe((data) => {
      this.rolSeleccionados = data.map((usuarioRol) => usuarioRol.idRol);
    });
  }

  public consultarCompania(): void {
    this.companiaService.consultarGeneral().subscribe((data) => {
      this.companiaList = data;

      this.usuario.idCompania = data[0].idCompania;

      this.consultarUsuario();
    });
  }

  public toDataURL(url: string, callback: any): void {
    const xhr = new XMLHttpRequest();
    xhr.onload = () => {
      const reader = new FileReader();
      reader.onloadend = () => {
        callback(reader.result);
      };
      reader.readAsDataURL(xhr.response);
    };
    xhr.open('GET', url);
    xhr.responseType = 'blob';
    xhr.send();
  }

  public fileChange(event: any): void {
    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]);

      reader.onload = () => {
        const base64Url = reader.result as string;
        this.usuario.imagenBase64 = base64Url;
        this.url = this.sanitizer.bypassSecurityTrustUrl(base64Url);
        this.usuario.imagenTipoMime = event.target.files[0].type;
        this.urlName = event.target.files[0].name;
        this.usuario.fotoPerfilEditada = true;
      };
    }
  }

  public async eliminarImagen(): Promise<void> {
    this.url = '';
    this.usuario.imagenBase64 = '';
    this.usuario.imagenTipoMime = '';
    this.urlName = '';
    this.usuario.fotoPerfilEditada = true;
  }

  public async agregar(): Promise<boolean> {
    let exito: boolean = false;
    await this.usuarioService.agregar(this.usuario).toPromise()
      .then((data) => {
        this.usuario.idUsuario = data ?? 0;
        this.presentAlertAddExito();
        exito = true;
      })
      .catch(() => {
        exito = false;
      })
      .finally(() => {
        this.btnSubmit = false;
      });

    return exito;
  }

  public async editar(): Promise<boolean> {
    this.usuario.contrasenaActualizada = this.usuario.contrasena;

    let exito: boolean = false;
    await this.usuarioService.editarAdministrador(this.usuario).toPromise()
      .then((data) => {
        this.presentAlertEditExito();
        exito = true;
      })
      .catch(() => {
        exito = false;
      })
      .finally(() => {
        this.btnSubmit = false;
      });

    return exito;
  }

  public limpiarFormulario(formulario: NgForm): void {
    this.toDataURL(this.urlImagenDefault, (myBase64: string) => {
      this.url = this.sanitize(myBase64);
    });
    formulario.reset();
  }

  public cancelar(): void {
    this.onClose(false);
  }

  public async enviarFormulario(formulario: NgForm): Promise<void> {
    this.btnSubmit = true;
    if (!this.usuario.idCompania && !this.esExterno) {
      this.usuario.idCompania = this.companiaList[0].idCompania;
    }

    if (this.usuario.sueldoDiario == null || this.usuario.sueldoDiario === undefined) {
      this.usuario.sueldoDiario = 0;
    }

    if (!this.usuario.diasPago) {
      this.usuario.diasPago = 0;
    }

    if (!formulario.valid) {
      Utileria.validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      return;
    }

    this.usuario.idsRol = this.rolSeleccionados;
    this.usuario.idsEspecialidad = this.especialidadSeleccionadas;

    let exito: boolean = false;
    if (this.accion === GeneralConstant.MODAL_ACCION_AGREGAR) {
      exito = await this.agregar();
    } else if (this.accion === GeneralConstant.MODAL_ACCION_EDITAR) {
      exito = await this.editar();
    }

    if (!exito) {
      return;
    }

    if (this.desdeExpediente) {
      this.onClose(this.usuario.idUsuario);
      
    }
    else {
      this.onClose(this.usuario.idUsuario);
      //this.onClose(true);
      //this.usuarioImagenService.actualizarImagen(this.url);
    }
  }

  public sanitize(url: string): any {
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }

  public consultarTiposDeUsuario(): any {
    this.tipoUsuarioService.consultarTiposUsuarioSelector().subscribe( (data) => {
      this.tipoUsuarioList = data;
      this.onTipoUsuarioChange(this.usuario.idTipoUsuario);
    });
  }

  public onTipoUsuarioChange(value: any): void {
    const seleccionado = this.tipoUsuarioList.find( (a) => a.idTipoUsuario == value );

    if (seleccionado !== undefined && this.tipoUsuarioList.length > 0 && value > 0) {
      if (seleccionado.clave === GeneralConstant.CLAVE_TIPO_USUARIO_EXTERNO) {
        this.esExterno = true;
      } else {
        this.esExterno = false;
      }
    }

  }

  public consultarPermisos(idUsuario: number) {
    this.usuarioLocacionService.consultarPorUsuario(idUsuario).subscribe(data => {
      this.permisosList = data;
    });
  }

  public guardarPermiso() {
    this.usuarioLocacion.idUsuario = this.usuario.idUsuario;

    if (!(this.usuarioLocacion.idCompania > 0)) {
      this.modalMensajeService.modalError("La compañía es requerida");
      return;
    }

    if (!(this.usuarioLocacion.idLocacion > 0)) {
      this.modalMensajeService.modalError("El Hospital es requerido");
      return;
    }

    if (!(this.usuarioLocacion.idPerfil > 0)) {
      this.modalMensajeService.modalError("El perfil es requerido");
      return;
    }

    if (this.usuarioLocacion.idUsuarioLocacion > 0) {
      this.usuarioLocacionService.editar(this.usuarioLocacion).subscribe(data => {
        this.modalMensajeService.modalExito('Permiso modificado exitosamente');
        this.usuarioLocacion = new UsuarioLocacion();
        this.consultarPermisos(this.usuario.idUsuario);
      });
    } else {
      this.usuarioLocacionService.agregar(this.usuarioLocacion).subscribe(data => {
        this.modalMensajeService.modalExito('Permiso agregado exitosamente');
        this.usuarioLocacion = new UsuarioLocacion();
        this.consultarPermisos(this.usuario.idUsuario);
      });
    }
  }

  public onGridClick(gridData: { accion: string; data: UsuarioLocacion }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editarPermiso(gridData.data);
    } else if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminarPermiso(gridData.data.idUsuarioLocacion);
    }
  }

  public editarPermiso(usuarioLocacion: UsuarioLocacion) {
    this.usuarioLocacion = usuarioLocacion;
    this.onCompaniaChange(this.usuarioLocacion.idCompania, true);
  }

  public eliminarPermiso(idUsuarioLocacion: number) {
    this.usuarioLocacionService.eliminar(idUsuarioLocacion).subscribe(data => {
      this.modalMensajeService.modalExito('Permiso eliminado exitosamente');
      this.consultarPermisos(this.usuario.idUsuario);
    });
  }

  // Afectación contable
  public onGridClickUsuarioRol(gridData: { accion: string; data: UsuarioRol }):void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editarUsuarioRol(gridData.data);
    } else if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminarUsuarioRol(gridData.data.idUsuarioRol);
    }
  }

  public consultarConceptos(): void {
    this.conceptoService.consultarTodosParaSelector().subscribe((data) => {
      this.conceptoList = data;
    });
  }

  public consultarUsuarioRolParaGrid(idUsuario: number):void {
    this.usuarioRolService.consultarPorUsuarioParaGrid(idUsuario).subscribe((data) => {
      this.usuarioRolList = data;
    });
  }

  public editarUsuarioRol(usuarioRol: UsuarioRol):void {
    this.usuarioRol = usuarioRol;
  }

  public eliminarUsuarioRol(idUsuarioRol: number):void {
    this.usuarioRolService.eliminar(idUsuarioRol).subscribe((data) => {
      this.modalMensajeService.modalExito('Usuario Rol eliminado exitosamente');
      this.consultarUsuarioRolParaGrid(this.usuario.idUsuario);
      this.consultarUsuarioRol();
    });
  }

  public guardarAfectacionContable(formulario: NgForm): void {
    if (!formulario.valid) {
      Utileria.validarCamposRequeridos(formulario);
      return;
    }

    this.usuarioRol.idUsuario = this.usuario.idUsuario;
    if (this.usuarioRol.idRol === null || this.usuarioRol.idRol == undefined || !(this.usuarioRol.idRol > 0)) {
      this.modalMensajeService.modalError('El rol es requerido');
      return;
    }

    if (this.usuarioRol.idUsuarioRol > 0) {
      this.usuarioRolService.editar(this.usuarioRol).subscribe((data) => {
        this.modalMensajeService.modalExito('Usuario Rol editado exitosamente');
        this.usuarioRol = new UsuarioRol();
        this.consultarUsuarioRolParaGrid(this.usuario.idUsuario);
        this.consultarUsuarioRol();
      })
    } else {
      this.usuarioRolService.agregar(this.usuarioRol).subscribe((data) => {
        this.modalMensajeService.modalExito('Usuario Rol agregado exitosamente');
        this.usuarioRol = new UsuarioRol();
        this.consultarUsuarioRolParaGrid(this.usuario.idUsuario);
        this.consultarUsuarioRol();
      });
    }

    formulario.reset();
  }

  private presentAlertAddExito(): Promise<Boolean> {
    return new Promise((resolve) => {
      this.alertifyService.presentAlert({
        header: 'Usuario registrado',
        subHeader: 'Se ha dado de alta el usuario correctamente',
        Icono: 'check',
        Color: 'primary',
        twoButtons: false,
        confirmButtonText: "De acuerdo",
        cancelButtonText: ''
      }, (result) => {
        if(result == "confirm"){
          resolve(true);
        } else {
          resolve(false);
        }
      });
    });
  }

  
  private presentAlertEditExito(): Promise<Boolean> {
    return new Promise((resolve) => {
      this.alertifyService.presentAlert({
        header: 'Usuario registrado',
        subHeader: 'Se ha editado el usuario correctamente',
        Icono: 'check',
        Color: 'primary',
        twoButtons: false,
        confirmButtonText: "De acuerdo",
        cancelButtonText: ''
      }, (result) => {
        if(result == "confirm"){
          resolve(true);
        } else {
          resolve(false);
        }
      });
    });
  }

  
}
