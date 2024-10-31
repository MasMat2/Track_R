import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EstadoSelectorDto } from '@dtos/catalogo/estado-selector-dto';
import { MunicipioSelectorDto } from '@dtos/catalogo/municipio-selector-dto';
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
import { UsuarioService } from '@http/seguridad/usuario.service';
import { Banco } from '@models/catalogo/banco';
import { CodigoPostal } from '@models/catalogo/codigo-postal';
import { Compania } from '@models/catalogo/compania';
import { Hospital } from '@models/catalogo/hospital';
import { HospitalLogotipo } from '@models/catalogo/hospital-logotipo';
import { Lada } from '@models/catalogo/lada';
import { ListaPrecio } from '@models/catalogo/lista-precio';
import { Pais } from '@models/catalogo/pais';
import { RegimenFiscal } from '@models/catalogo/regimen-fiscal';
import { Almacen } from '@models/inventario/almacen';
import { Usuario } from '@models/seguridad/usuario';
import { EncryptionService } from '@services/encryption.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import * as Utileria from '@utils/utileria';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable, Observer, of } from 'rxjs';
import { map, switchMap, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { CertificadoConfiguracionComponent } from '../certificado-configuracion/certificado-configuracion.component';

@Component({
  selector: 'app-locacion-formulario',
  templateUrl: './locacion-formulario.component.html',
  styleUrls: ['./locacion-formulario.component.scss']
})
export class HospitalFormularioComponent implements OnInit {
  @ViewChild('hospitalLogotipoInput', { static: false })
  public hospitalLogotipoInput: ElementRef;

  public urlBackend = environment.urlBackend;

  public TITULO_MODAL_ELIMINAR = 'Eliminar Imagen del hospital';
  public mensajeExitoEliminarImagen = 'El logotipo del hospital ha sido eliminado';

  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;
  public accion: string;
  public mensajeEditar = 'El hospital ha sido modificado';
  public mensajeAgregar = 'El hospital ha sido agregado';
  public btnSubmit = false;
  public hospital = new Hospital();

  public estadoList: EstadoSelectorDto[] = [];
  public bancoList: Banco[] = [];
  public paisList: Pais[] = [];
  public regimenFiscalList: RegimenFiscal[] = [];
  public usuarioList: Usuario[] = [];
  public companiaList: Compania[] = [];
  public listaPrecioList: ListaPrecio[] = [];
  public ladaList: Lada[] = [];
  public municipioList: MunicipioSelectorDto[] = [];
  public codigoPostalList: CodigoPostal[] = [];
  public almacenList: Almacen[] = [];

  public campoPais: number;
  public campoEstado: number;
  public campoMunicipio: number;
  public campoColonia: string;

  public campoListaPreciosDefault: number;
  public campoListaPreciosCompaEnLinea: number;

  public codigoPostalSeleccionado: CodigoPostal;
  public desactivarOnChange: boolean;

  public typeaheadLoading: boolean;
  public suggestions$: Observable<CodigoPostal[]>;

  public hospitalLogotipo?: HospitalLogotipo;

  public idAlmacenProduccion: number;
  public idAlmacenCaduco: number;

  public configLada = Object.assign(
    { labelField: 'claveNumero', valueField: 'idLada', searchField: ['claveNumero'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  public configEstado = Object.assign(
    { labelField: 'nombre', valueField: 'idEstado', searchField: ['nombre'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );
  public configPais = Object.assign(
    { labelField: 'nombre', valueField: 'idPais', searchField: ['nombre'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );
  public configBanco = Object.assign(
    { labelField: 'nombre', valueField: 'idBanco', searchField: ['nombre'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );
  public configUsuario = Object.assign(
    { labelField: 'nombreCompleto', valueField: 'idUsuario', searchField: ['nombre'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );
  public configCompania = Object.assign(
    { labelField: 'nombre', valueField: 'idCompania', searchField: ['nombre'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );
  public configListaPrecios = Object.assign(
    { labelField: 'nombre', valueField: 'idListaPrecio', searchField: ['nombre'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );
  public configRegimenFiscal = Object.assign(
    { labelField: 'claveNombre', valueField: 'idRegimenFiscal', searchField: ['claveNombre'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  public configMunicipio = Object.assign(
    { labelField: 'nombre', valueField: 'idMunicipio', searchField: ['nombre'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );
  public configListaPrecio = Object.assign(
    { labelField: 'nombre', valueField: 'idListaPrecio', searchField: ['nombre'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );
  public configAlmacen = Object.assign(
    { labelField: 'nombre', valueField: 'idAlmacen', searchField: ['nombre'], dropdownParent: 'body' },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT,
  );

  constructor(
    private modalMensajeService: MensajeService,
    private hospitalService: HospitalService,
    private estadoService: EstadoService,
    private municipioService: MunicipioService,
    private codigoPostalService: CodigoPostalService,
    private paisService: PaisService,
    private bancoService: BancoService,
    private usuarioServices: UsuarioService,
    private companiaServices: CompaniaService,
    private listaPrecioService: ListaPrecioService,
    private regimenFiscalService: RegimenFiscalService,
    private ladaService: LadaService,
    private hospitalLogotipoService: HospitalLogotipoService,
    private encryptionService: EncryptionService,
    private almacenService: AlmacenService,
    private router: Router,
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef
  ) {}

  public ngOnInit(): void {
    this.consultarLadas();
    this.consultarRegimenFiscal();
    this.consultarListaPrecios();
    this.consultarBanco();
    this.consultarAlmacen();
    this.consultarUsuario();
    this.consultarCompania();
    this.consultarCompaniaPorUsuario();

    this.route.queryParams.subscribe((params) => {
      this.accion = this.encryptionService.readUrlParams(params).accion;

      if (this.encryptionService.readUrlParams(params).accion === GeneralConstant.COMPONENT_ACCION_EDITAR) {
        this.hospital = JSON.parse(this.encryptionService.readUrlParams(params).data);
        this.consultarHospitalLogotipo(this.hospital.idHospital);

        this.campoPais = this.hospital.idPais;
        this.campoEstado = this.hospital.idEstado;
        this.campoMunicipio = this.hospital.idMunicipio;
        this.campoColonia = this.hospital.colonia;

        this.paisService.consultarTodosParaSelector().subscribe((paises) => {
          this.estadoService.consultarPorPaisParaSelector(this.hospital.idPais).subscribe((estados) => {
            this.municipioService.consultarPorEstadoParaSelector(this.hospital.idEstado)
            .subscribe((municipios) => {
              this.codigoPostalService.consultarPorCodigoPostal(this.hospital.codigoPostal)
              .subscribe((codigosPostales) => {
                this.paisList = paises;
                this.estadoList = estados;
                this.municipioList = municipios;
                this.codigoPostalList = codigosPostales;
              });
            });
          });
        });
      } else {
        this.hospital = new Hospital();
        this.consultarPaises();
      }
    });
  }

  public consultarAlmacen(): void {
    this.almacenService.consultarTodosParaSelector().subscribe(
      (data) => {
        this.almacenList = data;
      });
  }

  public consultarLadas(): void {
    this.ladaService.consultarTodosParaSelector().subscribe((data) => (this.ladaList = data));
  }

  public consultarCompania(): void {
    this.companiaServices.consultarGeneral().subscribe((data) => {
      this.companiaList = data;
    });
  }

  public consultarListaPrecios(): void {
    this.listaPrecioService.consultarTodosPorHospitalParaSelector().subscribe((data) => {
      this.listaPrecioList = data;
    });
  }

  public consultarRegimenFiscal(): void {
    this.regimenFiscalService.consultarTodosParaSelector().subscribe((data) => {
      data.forEach((d) => d.claveNombre = d.clave + ' - ' + d.nombre);
      this.regimenFiscalList = data;
    });
  }

  public consultarPaises(): void {
    this.paisService.consultarTodosParaSelector().subscribe((data) => {
      this.paisList = data;
    });
  }

  public consultarEstados(idPais: number): void {
    this.estadoService.consultarPorPaisParaSelector(idPais).subscribe((data) => {
      this.estadoList = data;
    });
  }

  public consultarMunicipios(idEstado: number): void {
    this.municipioService.consultarPorEstadoParaSelector(idEstado).subscribe((data) => {
      this.municipioList = data;
    });
  }

  public consultarColonias(idMunicipio: number): void {
    this.codigoPostalService.consultarPorMunicipio(idMunicipio).subscribe((data) => {
      this.codigoPostalList = data;
    });
  }

  public consultarBusquedaCodigoPostal(): void {
    this.suggestions$ = new Observable((observer: Observer<string>) => {
      observer.next(this.hospital.codigoPostal);
    }).pipe(
      switchMap((query: string) => {
        if (query && this.campoPais > 0) {
          return this.codigoPostalService
            .consultarPorPaisBusqueda(query, this.campoPais)
            .pipe(
              map((data) => data || []),
              tap((data) => {
                this.consultarColoniasPorCodigoPostal(query);
              }))
            ;
        }

        return of([]);
      })
    );
  }

  public consultarColoniasPorCodigoPostal(codigoPostal: string): void {
    if (codigoPostal.length > 4) {
      this.codigoPostalService.consultarPorCodigoPostal(codigoPostal).subscribe((data) => {
        this.codigoPostalList = data;
      });
    }
  }

  public onChangePais(event: number, hijos: any[]): void {
    this.estadoList = [];
    this.municipioList = [];
    this.codigoPostalList = [];

    this.hospital.codigoPostal = '';
    this.campoEstado = 0;
    this.campoMunicipio = 0;
    this.campoColonia = '';

    if (event > 0) {
      this.consultarEstados(event);
    }
  }

  public onChangeEstado(event: number, hijos: any[]): void {
    this.municipioList = [];
    this.codigoPostalList = [];

    this.campoMunicipio = 0;
    this.campoColonia = '';
    this.hospital.codigoPostal = '';

    if (event > 0) {
      this.consultarMunicipios(event);
    }
  }

  public onChangeMunicipio(event: number, hijos: any[]): void {
    this.codigoPostalList = [];

    this.campoColonia = '';
    this.hospital.codigoPostal = '';

    if (event > 0) {
      this.consultarColonias(event);
    }
  }

  public onChangeColonia(event: any): void {
    if (this.codigoPostalList.some((e) => e.colonia === event)) {
      const codigoPostal = this.codigoPostalList.find((e) => e.colonia === event);
      this.hospital.codigoPostal = codigoPostal?.codigoPostal1 ?? '';
    }
  }

  public onChangeCodigoPostal(event: any | null, hijos: NgModel[]): void {
    if (event.length < 4) {
      this.municipioList = [];
      this.codigoPostalList = [];

      this.campoEstado = 0;
      this.campoMunicipio = 0;
      this.campoColonia = '';
    } else {
      if (this.codigoPostalList.some((e) => e.codigoPostal1 === event)) {
        const codigoPostal = this.codigoPostalList.find((e) => e.codigoPostal1 === event);
        const idCodigoPostal = codigoPostal?.idCodigoPostal;

        if (idCodigoPostal === undefined) {
          return;
        }

        this.codigoPostalService.consultar(idCodigoPostal)
        .subscribe((data) => {
          this.codigoPostalSeleccionado = data;
          this.desactivarOnChange = true;

          this.municipioList = [];
          this.codigoPostalList = [];

          this.municipioService.consultarPorEstadoParaSelector(data.idEstado).subscribe((municipios) => {
            this.codigoPostalService.consultarPorCodigoPostal(data.codigoPostal1).subscribe((colonias) => {

              this.municipioList = municipios;
              this.codigoPostalList = colonias;

              this.campoEstado = data.idEstado;
              this.campoMunicipio = data.idMunicipio;
              this.campoColonia = '';

              setTimeout(() => {
                this.desactivarOnChange = false;
              }, 2000);
            });
          });
        });
      }
    }
  }

  public consultarCompaniaPorUsuario(): void {
    this.companiaServices.consultarPorUsuario().subscribe((data) => {
      this.hospital.IdCompania = data.idCompania;
    });
  }

  public consultarBanco(): void {
    this.bancoService.consultarTodosParaSelector().subscribe((data) => {
      this.bancoList = data;
    });
  }

  public consultarUsuario(): void {
    this.usuarioServices
      .consultarPorTipoUsuario(GeneralConstant.CLAVE_TIPO_USUARIO_INTERNO)
      .subscribe((data) => {
        this.usuarioList = data;
      });
  }

  public consultarHospitalLogotipo(idHospital: number): void {
    this.hospitalLogotipoService.consultarPorHospital(idHospital).subscribe((data) => {
      this.hospitalLogotipo = data;
    })
  }

  public limpiarFormulario(): void {
    this.hospital = new Hospital();
    this.accion = GeneralConstant.MODAL_ACCION_AGREGAR;
  }

  private async agregar(): Promise<void> {
    const idHospital = await this.hospitalService
      .agregar(this.hospital)
      .toPromise();

    if (this.hospitalLogotipo != null && this.hospitalLogotipo.imagenBase64 != null) {
      this.hospitalLogotipo.idHospital = idHospital ?? 0;

      await this.hospitalLogotipoService
        .agregar(this.hospitalLogotipo)
        .toPromise();
    }
  }

  private async editar(): Promise<void> {
    await this.hospitalService
      .editar(this.hospital)
      .toPromise();

    if (this.hospitalLogotipo !== undefined && this.hospitalLogotipo !== null && this.hospitalLogotipo.imagenBase64 != null) {
      this.hospitalLogotipo.idHospital = this.hospital.idHospital;

      await this.hospitalLogotipoService
        .agregar(this.hospitalLogotipo)
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

    this.hospital.idPais = this.campoPais;
    this.hospital.idEstado = this.campoEstado;
    this.hospital.idMunicipio = this.campoMunicipio;
    this.hospital.colonia = this.campoColonia;

    const acciones: { [key: string]: [Function, string] } = {
      [GeneralConstant.COMPONENT_ACCION_EDITAR]: [this.editar.bind(this), 'El hospital ha sido editado'],
      [GeneralConstant.COMPONENT_ACCION_AGREGAR]: [this.agregar.bind(this), 'El hospital ha sido agregado']
    };

    const [funcion, mensaje] = acciones[this.accion];

    try {
      await funcion();
    }
    catch(error) {
      console.error(error)
      this.btnSubmit = false;
      return;
    }
    this.modalMensajeService.modalExito(mensaje);
    this.regresar();
  }

  public regresar(): void {
    this.router.navigate(['administrador/configuracion-general/catalogo/locacion']);
  }

  public onFileChangeLogotipo(event: any): void {
    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]);

      this.hospitalLogotipo = new HospitalLogotipo();
      this.hospitalLogotipo.nombreImagen = event.target.files[0].name;
      this.hospitalLogotipo.tipoMime = event.target.files[0].type;

      reader.onload = (event: Event) => {
        const result = reader.result;

        if (result === null) {
          this.hospitalLogotipo!.imagenBase64 = '';
          return;
        }

        this.hospitalLogotipo!.imagenBase64 = result.toString().split(',')[1];
      };
    }
  }

  public eliminarLogotipo(): void {
    if (this.hospitalLogotipo === undefined || !this.hospitalLogotipo.idHospitalLogotipo) {
      this.hospitalLogotipo = undefined;
      this.hospitalLogotipoInput.nativeElement.value = '';
      return;
    }

    this.modalMensajeService
      .modalConfirmacion(
        '¿Desea eliminar el logotipo del hospital?',
        'Eliminar Logotipo de Hospital',
        GeneralConstant.ICONO_CRUZ
      )
      .then(() => {
        this.hospitalLogotipoService
          .eliminar(this.hospitalLogotipo!.idHospitalLogotipo)
          .subscribe(() => {
            this.hospitalLogotipo = undefined;
            this.hospitalLogotipoInput.nativeElement.value = '';
            this.modalMensajeService.modalExito('El logotipo ha sido eliminado');
          });
      })
      .catch(() => {});
  }

  public obtenerSrcLogotipo(hospitalLogotipo: HospitalLogotipo): string {
    return `data:${hospitalLogotipo.tipoMime};base64,${hospitalLogotipo.imagenBase64}`;
  }

  public configurarCertificados(): void {
    const initialState = {
      idLocacion: this.hospital.idHospital
    };

    this.bsModalRef = this.modalService.show(
      CertificadoConfiguracionComponent, {
      initialState, ... GeneralConstant.CONFIG_MODAL_DEFAULT
    });

    this.bsModalRef.content.onClose = (cerrar:boolean) => {
      this.bsModalRef.hide();
    }
  }
}
