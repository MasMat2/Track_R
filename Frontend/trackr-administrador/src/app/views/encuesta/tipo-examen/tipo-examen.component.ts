import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TipoExamenService } from '@http/examen/tipo-examen.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { TipoExamen } from '@models/examen/tipo-examen';
import { ACCESO_TIPO_EXAMEN } from '@utils/codigos-acceso/examen.acceso';
import { FORM_ACTION } from '@utils/constants/constants';
import { ICONO } from '@utils/constants/font-awesome-icons';
import { CONFIG_COLUMN_ACTION, GRID_ACTION } from '@utils/constants/grid';
import { MODAL_CONFIG } from '@utils/constants/modal';
import { ColDef, ICellRendererParams } from 'ag-grid-community';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { EncryptionService } from 'src/app/shared/services/encryption.service';
import { TipoExamenFormularioComponent } from './tipo-examen-formulario/tipo-examen-formulario.component';

@Component({
  selector: 'app-tipo-examen',
  templateUrl: './tipo-examen.component.html',
})
export class TipoExamenComponent implements OnInit {

  public readonly HEADER_GRID: string = 'Tipos de Examen';

  // Accesos
  protected tieneAccesoAgregar: boolean = false;
  protected readonly EDITAR_TIPO_EXAMEN: string = ACCESO_TIPO_EXAMEN.Editar;
  protected readonly ELIMINAR_TIPO_EXAMEN: string = ACCESO_TIPO_EXAMEN.Eliminar;
 
  // Grid
  public tipoExamenList: TipoExamen[] = [];

  private columnaDetalle: ColDef = Object.assign(
    {
      headerName: '',
        field: '',
      action: GRID_ACTION.Ver,
      cellRendererSelector: (params: ICellRendererParams) => {
        const component = {
          component: 'actionButton', //aqui es donde se asigna el icono
          params: { disabled: false },
        };
        return component;
      },
      resizable: false,
        sortable: false,
        filter: false,
        suppressMovable: true,
        lockPosition: true,
        pinned: 'right',
      minWidth: 44,
      maxWidth: 44,
    },
    CONFIG_COLUMN_ACTION
  );

  protected columns: ColDef[] = [
    {
      headerName: 'Clave',
      field: 'clave',
      minWidth: 150,
    },
    {
      headerName: 'Tipo de Examen',
      field: 'nombre',
      minWidth: 150,
    },
    {
      headerName: 'Cantidad de Reactivos',
      field: 'totalPreguntas',
      minWidth: 150,
    },
    {
      headerName: 'Duración Total (Minutos)',
      field: 'duracion',
      minWidth: 150,
    },
    this.columnaDetalle,
  ];

  constructor(
    private accesoService: AccesoService,
    private encryptionService: EncryptionService,
    private mensajeService: MensajeService,
    private modalService: BsModalService,
    private router: Router,
    private tipoExamenService: TipoExamenService
  ) {}

  public ngOnInit(): void {
    this.consultarGrid();
    this.consultarAccesoAgregar();
  }

  private consultarAccesoAgregar(): void {
    this.accesoService
      .tieneAcceso(ACCESO_TIPO_EXAMEN.Agregar)
      .subscribe((tieneAcceso: boolean) => {
        this.tieneAccesoAgregar = tieneAcceso;
      });
  }

  /**
   * Consulta la informacion del grid.
   */
  private consultarGrid() {
    this.tipoExamenService
      .consultarGeneral()
      .subscribe((tiposExamen: TipoExamen[]) => {
        this.tipoExamenList = tiposExamen;
      });
  }

  /**
   * Evento que se ejecuta al dar clic en algun boton del grid.
   */
  protected onGridClick(gridData: { accion: string; data: TipoExamen }) {
    if (gridData.accion === GRID_ACTION.Editar) {
      this.editar(gridData.data.idTipoExamen);
    } else if (gridData.accion === GRID_ACTION.Eliminar) {
      this.eliminar(gridData.data);
    } else if (gridData.accion === GRID_ACTION.Ver) {
      this.ver(gridData.data.idTipoExamen);
    }
  }

  /**
   * Muestra el modal de agregar un registro.
   */
  protected agregar() {
    const initialState = {
      accion: FORM_ACTION.Agregar,
      esEdicion: false,
      onClose: (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }

        bsModalRef.hide();
      },
    };

    const bsModalRef = this.modalService.show(
      TipoExamenFormularioComponent,
      {
        initialState: initialState,
        ...MODAL_CONFIG.Default,
      }
    );
  }

  /**
   * Muestra el modal de editar un registro y lo carga.
   */

  private editar(idTipoExamen: number) {
    const initialState = {
      accion: FORM_ACTION.Editar,
      esEdicion: true,
      idTipoExamen: idTipoExamen,
      onClose: (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }

        bsModalRef.hide();
      },
    };

    const bsModalRef = this.modalService.show(
      TipoExamenFormularioComponent,
      {
        initialState: initialState,
        ...MODAL_CONFIG.Default,
      }
    );
  }

  private eliminar(tipoExamen: TipoExamen) {
    const MENSAJE_EXITO: string = 'El tipo de cuestionario ha sido eliminado';
    const TITULO_MODAL: string = 'Eliminar Tipo de Cuestionario';
    const MENSAJE_CONFIRMACION: string = `¿Desea eliminar el tipo de cuestionario: <strong>${tipoExamen.nombre}</strong>?`;

    this.mensajeService
      .modalConfirmacion(
        MENSAJE_CONFIRMACION,
        TITULO_MODAL,
        ICONO.Cruz
      )
      .then(() => {
        this.tipoExamenService
          .eliminar(tipoExamen.idTipoExamen)
          .subscribe(() => {
            this.mensajeService.modalExito(MENSAJE_EXITO);
            this.consultarGrid();
          });
      });
  }

  private ver(idTipoExamen: number) {
    this.router.navigate(['/administrador/examen/tipo-examen/contenido-examen'], {
      queryParams: this.encryptionService.generateURL({
        idTipoExamen: idTipoExamen.toString(),
      }),
    });
  }
}
