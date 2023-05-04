import { GridOptions } from 'ag-grid-community';
import { Router } from '@angular/router';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { CompaniaService } from '@http/catalogo/compania.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { EncryptionService } from '@services/encryption.service';
import { GridGeneralComponent } from '@sharedComponents/grid-general/grid-general.component';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import { PerfilService } from '@http/seguridad/perfil.service';
import { Perfil } from '@models/seguridad/perfil';
import { SessionService } from '@services/session.service';

@Component({
  templateUrl: 'perfil.component.html'
})
export class PerfilComponent implements OnInit, AfterViewInit{
  @ViewChild('gridPerfil', { static: false }) gridPerfil: GridGeneralComponent;
  public HEADER_GRID = 'Perfiles';
  private MENSAJE_EXITO_ELIMINAR = 'Perfil eliminado exitosamente.';
  private TITULO_MODAL_ELIMINAR = 'Eliminar Perfil';
  public liquidacionList: any[];
  public fechaInicio = new Date();
  public fechaFin = new Date();
  public gridOptions: GridOptions;
  public idPerfilesSeleccionadas = [];
  public optionsFecha = GeneralConstant.CONFIG_DATEPICKER;
  public tieneAccesoAgregar = false;
  public EDITAR_PERFIL = CodigoAcceso.EDITAR_PERFIL;
  public ELIMINAR_PERFIL = CodigoAcceso.ELIMINAR_PERFIL;
  public idCompaniaUsuarioSesion: number;

  public columnaEditar = Object.assign(
    {
      action: GeneralConstant.GRID_ACCION_EDITAR,
      title: 'Editar',
      acceso: this.EDITAR_PERFIL,
      cellRendererSelector: (params: any) => {
        if (params.data.idCompania !== this.idCompaniaUsuarioSesion) {
          return null;
        }

        const component = { component: 'actionButton', params: { disabled: false } };
        return component;
      },
    },
    GeneralConstant.CONFIG_COLUMN_ACTION
  );

  public columnaEliminar = Object.assign(
    {
      action: GeneralConstant.GRID_ACCION_ELIMINAR,
      title: 'Eliminar',
      acceso: this.ELIMINAR_PERFIL,
      cellRendererSelector: (params: any) => {
        if (params.data.idCompania !== this.idCompaniaUsuarioSesion) {
          return null;
        }

        const component = { component: 'actionButton', params: { disabled: false } };
        return component;
      },
    },
    GeneralConstant.CONFIG_COLUMN_ACTION
  );

  public columns = [
    { headerName: 'Perfil', field: 'nombre', minWidth: 150 },
    { headerName: 'Tipo de Compañía', field: 'nombreTipoCompania', minWidth: 150 },
    { headerName: 'Jerarquía', field: 'nombreJerarquia', minWidth: 150 },
    this.columnaEditar,
    this.columnaEliminar
  ];

  constructor(
    private modalMensajeService: MensajeService,
    private router: Router,
    private perfilService: PerfilService,
    private encryptionService: EncryptionService,
    private accesoService: AccesoService,
    private sessionService: SessionService,
    private companiaService: CompaniaService
  ) {}

  public ngOnInit(): void {
    this.accesoService.tieneAcceso(CodigoAcceso.AGREGAR_PERFIL).subscribe((data) => {
      this.tieneAccesoAgregar = data;
    });
    this.consultarGrid();
  }

  public async ngAfterViewInit(): Promise<void> {
    const idCompania: number = this.sessionService.obtenerIdCompaniaUsuarioSesion() ?? 0;
    const companiaBase: boolean = await this.esCompaniaBase(idCompania);
    this.idCompaniaUsuarioSesion = idCompania;

    if (!companiaBase) {
      this.gridPerfil.gridOptions.columnApi.setColumnVisible('nombreTipoCompania', false);
    }
  }

  /**
   * Consulta la informacion del grid.
   */
  public consultarGrid() {
    this.perfilService.consultarPorCompania().subscribe((data) => {
      this.liquidacionList = data;
    });
  }

  /**
   * Método para indicar si la compañía consultada, es la compañía base.
   */
  private async esCompaniaBase(idCompania: number) {
    return this.companiaService.consultar(idCompania)
      .toPromise()
      .then((data) => data?.clave === GeneralConstant.CLAVE_COMPANIA_BASE)
      .catch(() => false);
  }

  /**
   * Evento que se ejecuta al dar clic en algun boton del grid.
   */
  public onGridClick(gridData: { accion: string; data: Perfil }) {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data.idPerfil);
    } else if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
    }
  }

  /**
   * Muestra la pantalla de editar un registro y lo carga.
   */
  public editar(idPerfil: number) {
    this.perfilService.consultar(idPerfil).subscribe((data) => {
      this.router.navigate(['/administrador/configuracion-general/perfil/editar'], {
        queryParams: this.encryptionService.generateURL({
          i: idPerfil.toString(),
          ij: data.idJerarquiaAcceso > 0 ? data.idJerarquiaAcceso.toString() : 0
        })
      });
    });
  }

  /**
   * Muestra un mensaje de confirmacion para eliminar el registro.
   */
  public eliminar(perfil: Perfil) {
    this.modalMensajeService
      .modalConfirmacion(
        '¿Desea eliminar el perfil <strong>' + perfil.nombre + '</strong>?',
        this.TITULO_MODAL_ELIMINAR,
        GeneralConstant.ICONO_CRUZ
      )
      .then((aceptar) => {
        this.perfilService.eliminar(perfil.idPerfil).subscribe((data) => {
          this.modalMensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
          this.consultarGrid();
        });
      });
  }

  /**
   * Muestra la pantalla de agregar un registro.
   */
  public agregar() {
    this.router.navigate(['/administrador/configuracion-general/perfil/agregar']);
  }
}
