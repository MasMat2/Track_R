import { GridOptions } from 'ag-grid-community';
import { Router } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Acceso } from '@models/seguridad/acceso';
import { AccesoService } from '@http/seguridad/acceso.service';
import { EncryptionService } from '@services/encryption.service';
import { CodigoAcceso } from 'src/app/shared/utils/codigo-acceso';
import { GridGeneralComponent } from 'src/app/shared/components/grid-general/grid-general.component';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { GeneralConstant } from 'src/app/shared/utils/general-constant';

@Component({
  templateUrl: 'acceso.component.html'
})
export class AccesoComponent implements OnInit {
  @ViewChild('gridAcceso', { static: false }) gridAcceso: GridGeneralComponent;
  public HEADER_GRID = 'Accesos';
  private MENSAJE_EXITO_ELIMINAR = 'Acceso eliminado exitosamente.';
  private TITULO_MODAL_ELIMINAR = 'Eliminar Acceso';

  public accesoList: Acceso[];
  public fechaInicio = new Date();
  public fechaFin = new Date();
  public gridOptions: GridOptions;
  public idAccesoesSeleccionadas = [];
  public optionsFecha = GeneralConstant.CONFIG_DATEPICKER;
  public tieneAccesoAgregar = false;
  public accesoEditar = CodigoAcceso.EDITAR_ACCESO;
  public accesoEliminar = CodigoAcceso.ELIMINAR_ACCESO;

  public columns = [
    {
      headerName: 'Clave',
      field: 'clave',
      minWidth: 150
    },
    {
      headerName: 'Nombre',
      field: 'nombre',
      minWidth: 150
    },
    {
      headerName: 'URL',
      field: 'url',
      minWidth: 150
    },
    {
      headerName: 'Tipo Acceso',
      field: 'tipoAcceso',
      minWidth: 150
    },
    {
      headerName: 'Acceso Padre',
      field: 'accesoPadre',
      minWidth: 150
    }
  ];

  constructor(
    private modalMensajeService: MensajeService,
    private router: Router,
    private accesoService: AccesoService,
    private encryptionService: EncryptionService
  ) {}

  ngOnInit(): void {
    this.consultarGrid();
    this.accesoService.tieneAcceso(CodigoAcceso.AGREGAR_ACCESO).subscribe((data) => {
      this.tieneAccesoAgregar = data;
    });
  }

  /**
   * Consulta la informacion del grid.
   */
  public consultarGrid() {
    this.accesoService.consultarGeneral().subscribe((data) => {
      this.accesoList = data;
    });
  }

  /**
   * Evento que se ejecuta al dar clic en algun boton del grid.
   */
  public onGridClick(gridData: { accion: string; data: Acceso }) {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data.idAcceso);
    } else if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
    }
  }

  /**
   * Muestra la pantalla de editar un registro y lo carga.
   */
  public editar(idCliente: number) {
    this.accesoService.consultar(idCliente).subscribe((data) => {
      this.router.navigate(['/administrador/configuracion-general/acceso/agregar'], {
        queryParams: this.encryptionService.generateURL({
          i: idCliente.toString()
        })
      });
    });
  }

  /**
   * Muestra un mensaje de confirmacion para eliminar el registro.
   */
  public eliminar(acceso: Acceso) {
    this.modalMensajeService
      .modalConfirmacion(
        '¿Desea eliminar el acceso <strong>' + acceso.nombre + '</strong>?',
        this.TITULO_MODAL_ELIMINAR,
        GeneralConstant.ICONO_CRUZ
      )
      .then((aceptar) => {
        this.accesoService.eliminar(acceso.idAcceso).subscribe((data) => {
          this.modalMensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
          this.consultarGrid();
        });
      });
  }

  /**
   * Muestra la pantalla de agregar un registro.
   */
  public agregar() {
    this.router.navigate(['/administrador/configuracion-general/acceso/agregar']);
  }
}
