import { Component, OnInit } from '@angular/core';
import { AsignaturaService } from '@http/examen/asignatura.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Asignatura } from '@models/examen/asignatura';
import { ACCESO_ASIGNATURA } from '@utils/codigos-acceso/examen.acceso';
import { FORM_ACTION } from '@utils/constants/constants';
import { ICONO } from '@utils/constants/font-awesome-icons';
import { GRID_ACTION } from '@utils/constants/grid';
import { MODAL_CONFIG } from '@utils/constants/modal';
import { ColDef } from 'ag-grid-community';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { AsignaturaFormularioComponent } from './asignatura-formulario/asignatura-formulario.component';

@Component({
  selector: 'app-asignatura',
  templateUrl: './asignatura.component.html',
})
export class AsignaturaComponent implements OnInit {
  protected readonly NOMBRE_ENTIDAD: string = 'Cuestionarios';

  // Accesos
  protected tieneAccesoAgregar: boolean = false;
  protected readonly EDITAR_ASIGNATURA: string = ACCESO_ASIGNATURA.Editar;
  protected readonly ELIMINAR_ASIGNATURA: string = ACCESO_ASIGNATURA.Eliminar;

  // Grid
  protected asignaturas: Asignatura[] = [];

  public columns: ColDef[] = [
    { headerName: 'Clave', field: 'idAsignatura', maxWidth: 150, },
    { headerName: 'Estatus', field: 'estatus', maxWidth: 150, 
    valueGetter: (params: any) => params.data.estatus ? 'Activo' : 'Inactivo' },
  { headerName: 'Fecha Alta', field: 'fechaAlta', maxWidth: 200,
    cellRenderer: (data: any) => { return data.value ? (new Date(data.value)).toLocaleDateString() : '';} },
    { headerName: 'Cuestionarios', field: 'descripcion', minWidth: 300, },
  ];

  constructor(
    private accesoService: AccesoService,
    private asignaturaService: AsignaturaService,
    private bsModalRef: BsModalRef,
    private mensajeService: MensajeService,
    private modalService: BsModalService,
  ) {}

  public ngOnInit(): void {
    this.consultarGrid();
    this.accesoService
      .tieneAcceso(ACCESO_ASIGNATURA.Agregar)
      .subscribe((tieneAcceso: boolean) => {
        this.tieneAccesoAgregar = tieneAcceso;
      });
  }

  /**
   * Consulta la informacion del grid.
   */
  public consultarGrid(): void {
    this.asignaturaService
      .consultarGeneral()
      .subscribe((asignaturas: Asignatura[]) => {
        this.asignaturas = asignaturas;
      });
  }

  /**
   * Evento que se ejecuta al dar clic en algun boton del grid.
   */
  public onGridClick(gridData: { accion: string; data: Asignatura }) {
    const acciones: { [key: string]: () => void } = {
      [GRID_ACTION.Editar]: () => this.editar(gridData.data.idAsignatura),
      [GRID_ACTION.Eliminar]: () => this.eliminar(gridData.data),
      [GRID_ACTION.Ver]: () => {},
    };

    acciones[gridData.accion]();
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
        this.bsModalRef.hide();
      },
    };

    this.bsModalRef = this.modalService.show(
      AsignaturaFormularioComponent,
      {
        initialState,
        ...MODAL_CONFIG.Large,
      }
    );

  }

  /**
   * Muestra el modal de editar un registro y lo carga.
   */

  private editar(idAsignatura: number): void {
    const initialState = {
      accion: FORM_ACTION.Editar,
      esEdicion: true,
      idAsignatura: idAsignatura,
      onClose: (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }
        this.bsModalRef.hide();
      },
    };

    this.bsModalRef = this.modalService.show(AsignaturaFormularioComponent, {
      initialState,
      ...MODAL_CONFIG.Large,
    });
  }

  private eliminar(asignatura: Asignatura): void {
    const MENSAJE_EXITO: string = 'El Cuestionario ha sido eliminado';
    const TITULO_MODAL: string = 'Eliminar Cuestionario';
    const MENSAJE_CONFIRMACION: string = `¿Desea eliminar el Cuestionario: <strong>${asignatura.descripcion}</strong>?`;

    this.mensajeService
      .modalConfirmacion(
        MENSAJE_CONFIRMACION,
        TITULO_MODAL,
        ICONO.Cruz
      )
      .then(() => {
        this.asignaturaService
          .eliminar(asignatura.idAsignatura)
          .subscribe(() => {
            this.mensajeService.modalExito(MENSAJE_EXITO);
            this.consultarGrid();
          });
      });
  }
}
