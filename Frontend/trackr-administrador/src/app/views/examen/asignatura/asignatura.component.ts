import { Component, OnInit } from '@angular/core';
import { AsignaturaService } from '@http/examen/asignatura.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Asignatura } from '@models/examen/asignatura';
import { ACCESO_ASIGNATURA } from '@utils/codigos-acceso/examen.acceso';
import { GeneralConstant } from '@utils/general-constant';
import { ColDef } from 'ag-grid-community';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { AsignaturaFormularioComponent } from './asignatura-formulario/asignatura-formulario.component';

@Component({
  selector: 'app-asignatura',
  templateUrl: './asignatura.component.html',
})
export class AsignaturaComponent implements OnInit {
  protected readonly NOMBRE_ENTIDAD: string = 'Asignaturas';

  // Accesos
  protected tieneAccesoAgregar: boolean = true;
  protected readonly EDITAR_ASIGNATURA: string = ACCESO_ASIGNATURA.Editar;
  protected readonly ELIMINAR_ASIGNATURA: string = ACCESO_ASIGNATURA.Eliminar;

  // Grid
  protected asignaturas: Asignatura[] = [];

  public columns: ColDef[] = [
    {
      headerName: 'Clave',
      field: 'clave',
      minWidth: 150,
    },
    {
      headerName: 'Descripción',
      field: 'descripcion',
      minWidth: 150,
    },
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
    // this.accesoService
    //   .tieneAcceso(ACCESO_ASIGNATURA.Agregar)
    //   .subscribe((tieneAcceso) => {
    //     // this.tieneAccesoAgregar = tieneAcceso;
    //     this.tieneAccesoAgregar = true;
    //   });
  }

  /**
   * Consulta la informacion del grid.
   */
  public consultarGrid(): void {
    this.asignaturaService
      .consultarGeneral()
      .subscribe((asignaturas) => {
        this.asignaturas = asignaturas;
      });
  }

  /**
   * Evento que se ejecuta al dar clic en algun boton del grid.
   */
  public onGridClick(gridData: { accion: string; data: Asignatura }) {
    const acciones: { [key: string]: () => void } = {
      [GeneralConstant.GRID_ACCION_EDITAR]: () => this.editar(gridData.data.idAsignatura),
      [GeneralConstant.GRID_ACCION_ELIMINAR]: () => this.eliminar(gridData.data),
      [GeneralConstant.GRID_ACCION_VER]: () => {},
    };

    acciones[gridData.accion]();
  }

  /**
   * Muestra el modal de agregar un registro.
   */
  protected agregar(): void {
    const initialState = {
      accion: GeneralConstant.MODAL_ACCION_AGREGAR,
      esEdicion: false,
      onClose: (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }
        this.bsModalRef.hide();
      },
    };

    this.bsModalRef = this.modalService.show(AsignaturaFormularioComponent, {
      initialState,
      ...GeneralConstant.CONFIG_MODAL_DEFAULT,
    });
  }

  /**
   * Muestra el modal de editar un registro y lo carga.
   */

  private editar(idAsignatura: number): void {
    const initialState = {
      accion: GeneralConstant.MODAL_ACCION_EDITAR,
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
      ...GeneralConstant.CONFIG_MODAL_DEFAULT,
    });
  }

  private eliminar(asignatura: Asignatura): void {
    const MENSAJE_EXITO: string = 'La asignatura ha sido eliminada';
    const TITULO_MODAL: string = 'Eliminar Asignatura';
    const MENSAJE_CONFIRMACION: string = `¿Desea eliminar la asignatura: <strong>${asignatura.descripcion}</strong>?`;

    this.mensajeService
      .modalConfirmacion(
        MENSAJE_CONFIRMACION,
        TITULO_MODAL,
        GeneralConstant.ICONO_CRUZ
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
