import { Component, OnInit } from '@angular/core';
import { NivelExamenService } from '@http/examen/nivel-examen.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { NivelExamen } from '@models/examen/nivel-examen';
import { ACCESO_NIVEL_EXAMEN } from '@utils/codigos-acceso/examen.acceso';
import { FORM_ACTION } from '@utils/constants/constants';
import { ICONO } from '@utils/constants/font-awesome-icons';
import { GRID_ACTION } from '@utils/constants/grid';
import { MODAL_CONFIG } from '@utils/constants/modal';
import { ColDef } from 'ag-grid-community';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { NivelExamenFormularioComponent } from './nivel-examen-formulario/nivel-examen-formulario.component';

@Component({
  selector: 'app-nivel-examen',
  templateUrl: './nivel-examen.component.html',
})
export class NivelExamenComponent implements OnInit {
  protected readonly HEADER_GRID: string = 'Nivel Examen';

  // Accesos
  protected tieneAccesoAgregar: boolean = false;
  protected readonly ACCESO_EDITAR: string = ACCESO_NIVEL_EXAMEN.Editar;
  protected readonly ACCESO_ELIMINAR: string = ACCESO_NIVEL_EXAMEN.Eliminar;

  // Grid
  protected nivelExamenList: NivelExamen[] = [];

  protected columns: ColDef[] = [
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
    private mensajeService: MensajeService,
    private modalService: BsModalService,
    private nivelExamenService: NivelExamenService,
  ) {}

  ngOnInit(): void {
    this.consultarGrid();
    this.consultarAccesoAgregar();
  }

  private consultarAccesoAgregar(): void {
    this.accesoService
      .tieneAcceso(ACCESO_NIVEL_EXAMEN.Agregar)
      .subscribe((tieneAcceso: boolean) => {
        this.tieneAccesoAgregar = tieneAcceso;
      });
  }

  /**
   * Consulta la informacion del grid.
   */
  private consultarGrid(): void {
    this.nivelExamenService
      .consultarGeneral()
      .subscribe((nivelesExamen: NivelExamen[]) => {
        this.nivelExamenList = nivelesExamen;
      });
  }

  /**
   * Evento que se ejecuta al dar clic en algun boton del grid.
   */
  protected onGridClick(gridData: { accion: string; data: NivelExamen }): void {
    if (gridData.accion === GRID_ACTION.Editar) {
      this.editar(gridData.data.idNivelExamen);
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
      onClose: (actualizar: boolean) => {
        if (actualizar) {
          this.consultarGrid();
        }

        bsModalRef.hide();
      }
    };

    const bsModalRef = this.modalService.show(
      NivelExamenFormularioComponent,
      {
        initialState: initialState,
        ...MODAL_CONFIG.Default
      }
    );
  }

  /**
   * Muestra el modal de editar un registro y lo carga.
   */

  private editar(idNivelExamen: number): void {
    const initialState = {
      accion: FORM_ACTION.Editar,
      esEdicion: true,
      idNivelExamen: idNivelExamen,
      onClose: (actualizar: boolean) => {
        if (actualizar) {
          this.consultarGrid();
        }

        bsModalRef.hide();
      }
    };

    const bsModalRef = this.modalService.show(
      NivelExamenFormularioComponent,
      {
        initialState: initialState,
        ...MODAL_CONFIG.Default
      }
    );
  }

  private eliminar(nivelExamen: NivelExamen): void {
    const MENSAJE_EXITO: string = 'El nivel de examen ha sido eliminado';
    const TITULO_MODAL: string = 'Eliminar Nivel Examen';
    const MENSAJE_CONFIRMACION: string = `¿Desea eliminar el nivel de examen: <strong>${nivelExamen.descripcion}</strong>?`;

    this.mensajeService
      .modalConfirmacion(MENSAJE_CONFIRMACION, TITULO_MODAL, ICONO.Cruz)
      .then(() => {
        this.nivelExamenService
          .eliminar(nivelExamen.idNivelExamen)
          .subscribe(() => {
            this.mensajeService.modalExito(MENSAJE_EXITO);
            this.consultarGrid();
          });
      });
  }
}
