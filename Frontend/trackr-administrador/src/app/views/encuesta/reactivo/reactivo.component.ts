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
import { ReactivoFormularioComponent } from './reactivo-formulario/reactivo-formulario.component';

@Component({
  selector: 'app-reactivo',
  templateUrl: './reactivo.component.html',
})
export class ReactivoComponent implements OnInit {

  protected readonly HEADER_GRID: string = 'Reactivos';

  // Accesos
  protected tieneAccesoAgregar: boolean = false;
  protected readonly EDITAR_ASIGNATURA: string = ACCESO_REACTIVO.Editar;
  protected readonly ELIMINAR_ASIGNATURA: string = ACCESO_REACTIVO.Eliminar;

  // Grid
  protected reactivoList: Reactivo[] = [];

  protected columns: ColDef[] = [
    {
      headerName: 'Clave',
      field: 'clave',
      minWidth: 50,
      width: 50
    },
    {
      headerName: 'Asignatura',
      field: 'asignatura',
      minWidth: 70,
      width: 70
    },
    {
      headerName: 'Nivel',
      field: 'nivelExamen',
      minWidth: 70,
      width: 70
    },
    {
      headerName: 'Pregunta',
      field: 'pregunta',
      minWidth: 120,
      width: 120,
      cellStyle: { 'white-space': 'normal' },
    },
    {
      headerName: 'Respuestas',
      field: 'respuesta',
      minWidth: 200,
      width: 200,
      cellStyle: { 'white-space': 'break-spaces' },
    },
    {
      headerName: 'Respuesta Correcta',
      field: 'respuestaCorrecta',
      minWidth: 80,
      width: 80,
    },
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
      ReactivoFormularioComponent,
      {
        initialState: initialState,
        ...MODAL_CONFIG.Default
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
      ReactivoFormularioComponent,
      {
        initialState: initialState,
        ...MODAL_CONFIG.Default
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
}
