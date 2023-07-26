import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Compania } from '@models/catalogo/compania';
import { TipoCompania } from '@models/catalogo/tipo-compania';
import { AccesoService } from '@http/seguridad/acceso.service';
import { CompaniaService } from '@http/catalogo/compania.service';
import { TipoCompaniaService } from '@http/catalogo/tipo-compania.service';
import { EncryptionService } from '@services/encryption.service';
import { ACCESO_COMPANIA } from 'src/app/shared/utils/codigos-acceso/catalogo.accesos';
import { GeneralConstant } from 'src/app/shared/utils/general-constant';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';

@Component({
  selector: 'app-compania',
  templateUrl: './compania.component.html'
})
export class CompaniaComponent implements OnInit {

  private readonly RUTA_FORM: string = 'administrador/configuracion-general/catalogo/compania/form'

  // Filtros
  public isCollapsed = true;
  public filtro: any = {};
  public buscando = false;
  private MENSAJE_EXITO_BUSQUEDA = 'La búsqueda ha sido realizada';

  // DropDown
  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

  public tieneAccesoAgregar = false;

  public accesoEditar = ACCESO_COMPANIA.Editar;
  public accesoEliminar = ACCESO_COMPANIA.Eliminar;
  public HEADER_GRID = 'Compañía';
  private MENSAJE_EXITO_ELIMINAR = 'La compañía ha sido eliminada';
  private TITULO_MODAL_ELIMINAR = 'Eliminar Compañía';

  public companiaList: Compania[] = [];
  public tipoCompaniaList: TipoCompania[];


  public configTipoCompania = Object.assign(
    { labelField: 'nombre', valueField: 'idTipoCompania', searchField: ['nombre'], dropdownParent: 'body' },
    {...GeneralConstant.CONFIG_DROPDOWN_DEFAULT}
  );

  public columns = [
    { headerName: 'Núm. Compañía', field: 'idCompania', minWidth: 150, sort: 'asc' },
    { headerName: 'Compañía', field: 'nombre', minWidth: 150 },
    { headerName: 'Ciudad', field: 'ciudad', minWidth: 150 },
  ];

  constructor(
    private modalMensajeService: MensajeService,
    private companiaService: CompaniaService,
    private accesoService: AccesoService,
    private encryptionService: EncryptionService,
    private tipoCompaniaService: TipoCompaniaService,
    private router: Router
  ) { }

  ngOnInit() {
    this.accesoService.tieneAcceso(ACCESO_COMPANIA.Agregar).subscribe((data) => {
      this.tieneAccesoAgregar = data;
    });
    this.filtro.idTipoCompania = 0;
    this.consultarGrid();
    this.consultarTipoCompanias();

  }

  consultarGrid() {
    this.companiaService.consultarTodosParaGrid(this.filtro).subscribe((data) => {
      this.companiaList = data;
    });
  }
  consultarTipoCompanias() {
    this.tipoCompaniaService.consultarParaSelector().subscribe((data) => {
      this.tipoCompaniaList = data;
    });
  }


  onGridClick(gridData: { accion: string; data: Compania }) {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data.idCompania);
    } else if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
    }
  }

  agregar() {
    this.router.navigate([this.RUTA_FORM], {
      queryParams: this.encryptionService.generateURL({
        accion: GeneralConstant.MODAL_ACCION_AGREGAR
      })
    });
  }

  editar(idHospital: number) {
    this.router.navigate([this.RUTA_FORM], {
      queryParams: this.encryptionService.generateURL({
        idCompania: idHospital.toString(),
        accion: GeneralConstant.MODAL_ACCION_EDITAR
      })
    });
  }

  eliminar(compania: Compania) {
    this.modalMensajeService
      .modalConfirmacion(
        '¿Desea eliminar la compañía <strong>' + compania.nombre + '</strong>?',
        this.TITULO_MODAL_ELIMINAR,
        GeneralConstant.ICONO_CRUZ
      )
      .then((_: any) => {
        this.companiaService.eliminar(compania.idCompania).subscribe((data) => {
          this.modalMensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
          this.consultarGrid();
        });
      });
  }

  public buscar(mostrarMensaje: boolean): void {
    if (this.filtro.rfc === '') {
      this.filtro.rfc = undefined;
    }

    if (this.filtro.nombre === '') {
    this.filtro.folio = undefined;
    }

    if (this.filtro.idTipoCompania == undefined || this.filtro.idTipoCompania.length <= 0) {
      this.filtro.idTipoCompania = 0;
    }

    this.buscando = true;
    const facturaPromise = this.companiaService
      .consultarTodosParaGrid(this.filtro)
      .toPromise();

    Promise.all([facturaPromise])
      .then((result) => {
        if (mostrarMensaje) {
          this.modalMensajeService.modalExito(this.MENSAJE_EXITO_BUSQUEDA);
        }
        this.companiaList = result[0] ?? [];
      })
      .finally(() => this.buscando = false);
  }

  public limpiar(): void {
    this.filtro.nombre = '';
    this.filtro.rfc = '';
    this.filtro.idTipoCompania = 0;
    this.buscar(false);
  }

}
