import { Component, OnInit } from '@angular/core';
import { ReactivoService } from '@http/examen/reactivo.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Reactivo } from '@models/examen/reactivo';
import { ACCESO_REACTIVO } from '@utils/codigos-acceso/examen.acceso';
import { FORM_ACTION } from '@utils/constants/constants';
import { ICONO } from '@utils/constants/font-awesome-icons';
import { GRID_ACTION } from '@utils/constants/grid';
import { MODAL_CONFIG } from '@utils/constants/modal';
import { ColDef } from 'ag-grid-community';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { Reactivo1FormularioComponent } from './reactivo-formulario/reactivo-formulario.component';
import { GeneralConstant } from '@utils/general-constant';

@Component({
  selector: 'app-reactivo',
  templateUrl: './reactivo.component.html',
})
export class Reactivo1Component implements OnInit {

  protected readonly HEADER_GRID: string = 'Reactivos';

  // Accesos
  protected tieneAccesoAgregar: boolean = false;
  protected readonly EDITAR_ASIGNATURA: string = ACCESO_REACTIVO.Editar;
  protected readonly ELIMINAR_ASIGNATURA: string = ACCESO_REACTIVO.Eliminar;

  // Grid
  protected reactivoList: Reactivo[] = [];

  protected verRespuestasButton: any = {
    headerName: '',
    field: '',
    cellRenderer: 'actionButton',
    minWidth: 20,
    maxWidth: 40,
    resizable: true,
    sortable: false,
    filter: false,
    action: GeneralConstant.GRID_ACCION_VER,
    pinned: undefined
  };

  protected columns: ColDef[] = [
    {
      headerName: '',
      field: 'idReactivo',
      sort: 'desc',
      hide: true
    },
    {
      headerName: 'Clave',
      field: 'clave',
      minWidth: 80,
    },
    {
      headerName: 'Asignatura',
      field: 'asignatura',
      minWidth: 80,
    },
    {
      headerName: 'Nivel de Evaluación',
      field: 'nivelExamen',
      minWidth: 80,
    },
    {
      headerName: 'Pregunta',
      field: 'pregunta',
      minWidth: 150,
      cellStyle: { 'white-space': 'normal' },
    },
    {
      headerName: 'Respuestas',
      field: 'respuesta',
      minWidth: 80,
      cellStyle: { 'white-space': 'break-spaces' },
    },
    {
      headerName: 'Respuesta Correcta',
      field: 'respuestaCorrecta',
      minWidth: 60,
      width: 60,
    },
    this.verRespuestasButton
  ];

  constructor(
    private accesoService: AccesoService,
    private mensajeService: MensajeService,
    private modalService: BsModalService,
    private reactivoService: ReactivoService,
  ) {}

  public ngOnInit(): void {
    this.consultarGrid();
    this.consultarAccesoAgregar();
  }

  private consultarAccesoAgregar() {
    this.accesoService
      .tieneAcceso(ACCESO_REACTIVO.Agregar)
      .subscribe((tieneAcceso: boolean) => {
        this.tieneAccesoAgregar = tieneAcceso;
      });
  }

  /**
   * Consulta la informacion del grid.
   */
  private consultarGrid() {
    this.reactivoService
      .consultarGeneral()
      .subscribe((reactivos: Reactivo[]) => {
        this.reactivoList = reactivos;
      });
  }

  /**
   * Evento que se ejecuta al dar clic en algun boton del grid.
   */
  protected onGridClick(gridData: { accion: string; data: Reactivo }) {
    if (gridData.accion === GRID_ACTION.Editar) {
      this.editar(gridData.data.idReactivo);
    }
    else if (gridData.accion === GRID_ACTION.Eliminar) {
      this.eliminar(gridData.data);
    }
    else if( gridData.accion === GRID_ACTION.Ver){
      this.verRespuestas(gridData.data.idReactivo)
    }
  }

  /**
   * Muestra el modal de agregar un registro.
   */
  protected agregar(): void {
    const initialState = {
      accion: FORM_ACTION.Agregar,
      esEdicion: false,
      onClose: (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }

        bsModalRef.hide();
      }
    };

    const bsModalRef = this.modalService.show(
      Reactivo1FormularioComponent,
      {
        initialState: initialState,
        ...MODAL_CONFIG.Large
      }
    );
  }

  /**
   * Muestra el modal de editar un registro y lo carga.
   */
  private editar(idReactivo: number) {
    const initialState = {
      accion: FORM_ACTION.Editar,
      esEdicion: true,
      idReactivo: idReactivo,
      onClose: (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }

        bsModalRef.hide();
      }
    };

    const bsModalRef = this.modalService.show(
      Reactivo1FormularioComponent,
      {
        initialState: initialState,
        ...MODAL_CONFIG.Large
      }
    );
  }

  private eliminar(reactivo: Reactivo) {
    const MENSAJE_EXITO: string = 'El reactivo ha sido eliminado';
    const TITULO_MODAL: string = 'Eliminar Reactivo';
    const MENSAJE_CONFIRMACION: string = `¿Desea eliminar la pregunta: <strong>${reactivo.pregunta}</strong>?`;

    this.mensajeService
      .modalConfirmacion(
        MENSAJE_CONFIRMACION,
        TITULO_MODAL,
        ICONO.Cruz
      )
      .then(() => {
        this.reactivoService
          .eliminar(reactivo.idReactivo)
          .subscribe(() => {
            this.mensajeService.modalExito(MENSAJE_EXITO);
            this.consultarGrid();
          });
      });
  }

  private verRespuestas(idReactivo: number) {
    const initialState = {
      accion: FORM_ACTION.Editar,
      esEdicion: true,
      idReactivo: idReactivo,
      soloRespuestas: false,
      onClose: (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }

        bsModalRef.hide();
      }
    };

    const bsModalRef = this.modalService.show(
      Reactivo1FormularioComponent,
      {
        initialState: initialState,
        ...MODAL_CONFIG.Medium
      }
    );
  }
}
