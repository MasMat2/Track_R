import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ContenidoExamenService } from '@http/examen/contenido-examen.service';
import { TipoExamenService } from '@http/examen/tipo-examen.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { ContenidoExamen } from '@models/examen/contenido-examen';
import { TipoExamen } from '@models/examen/tipo-examen';
import { ACCESO_TIPO_EXAMEN } from '@utils/codigos-acceso/examen.acceso';
import { GRID_ACTION } from '@utils/constants/grid';
import { ColDef } from 'ag-grid-community';
import { BsModalService } from 'ngx-bootstrap/modal';
import { first, lastValueFrom } from 'rxjs';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { EncryptionService } from 'src/app/shared/services/encryption.service';
import { ContenidoExamenFormularioComponent } from './contenido-examen-formulario/contenido-examen-formulario.component';
import { FORM_ACTION } from '@utils/constants/constants';
import { MODAL_CONFIG } from '@utils/constants/modal';
import { ICONO } from '@utils/constants/font-awesome-icons';

@Component({
  selector: 'app-contenido-examen',
  templateUrl: './contenido-examen.component.html',
})
export class ContenidoExamenComponent implements OnInit {
  protected readonly HEADER_GRID: string = 'Contenido del cuestionario';

  // Inputs
  public idTipoExamen: number;
  protected tipoExamen: TipoExamen = new TipoExamen();

  // Accesos
  protected tieneAccesoAgregar: boolean = false;
  protected readonly EDITAR_TIPO_EXAMEN = ACCESO_TIPO_EXAMEN.Editar;
  protected readonly ELIMINAR_TIPO_EXAMEN = ACCESO_TIPO_EXAMEN.Eliminar;

  // Grid
  protected contenidoExamenList: ContenidoExamen[] = [];

  protected columns: ColDef[] = [
    { headerName: 'Clave', field: 'idContenidoExamen', minWidth: 150, },
    { headerName: 'Cuestionario', field: 'asignatura', minWidth: 150, },
    { headerName: 'Complejidad', field: 'nivelExamen', minWidth: 150, },
    { headerName: 'Cantidad de Reactivos', field: 'totalPreguntas', minWidth: 150, },
    { headerName: 'Duración Total (Minutos)', field: 'duracion', minWidth: 150, },
  ];

  constructor(
    private accesoService: AccesoService,
    private contenidoExamenService: ContenidoExamenService,
    private encryptionService: EncryptionService,
    private mensajeService: MensajeService,
    private modalService: BsModalService,
    private route: ActivatedRoute,
    private router: Router,
    private tipoExamenService: TipoExamenService,
  ) {}

  public async ngOnInit(): Promise<void> {
    await this.consultarQueryParams();

    this.consultarTipoExamen(this.idTipoExamen);
    this.consultarAccesoAgregar();

    this.consultarGrid();
  }

  private consultarAccesoAgregar(): void {
    this.accesoService
      .tieneAcceso(ACCESO_TIPO_EXAMEN.Agregar)
      .subscribe((tieneAcceso: boolean) => {
        this.tieneAccesoAgregar = tieneAcceso;
      });
  }

  private async consultarQueryParams(): Promise<void> {
    const queryParams = await lastValueFrom(this.route.queryParams.pipe(first()));
    const params = this.encryptionService.readUrlParams(queryParams);

    this.idTipoExamen = params['idTipoExamen'];
  }

  private consultarTipoExamen(idTipoExamen: number): void {
    this.tipoExamenService
      .consultar(idTipoExamen)
      .subscribe((tipoExamen: TipoExamen) => {
        this.tipoExamen = tipoExamen;
      });
  }

  /**
   * Consulta la informacion del grid.
   */
  private consultarGrid(): void {
    this.contenidoExamenService
      .consultarGeneral(this.idTipoExamen)
      .subscribe((contenidoExamen: ContenidoExamen[]) => {
        this.contenidoExamenList = contenidoExamen;
      });
  }

  /**
   * Evento que se ejecuta al dar clic en algun boton del grid.
   */
  protected onGridClick(gridData: { accion: string; data: ContenidoExamen }): void {
    if (gridData.accion === GRID_ACTION.Editar) {
      this.editar(gridData.data.idContenidoExamen);
    } else if (gridData.accion === GRID_ACTION.Eliminar) {
      this.eliminar(gridData.data);
    } else if (gridData.accion === GRID_ACTION.Ver) {
      //this.ver(gridData.data.idContenidoExamen);
    }
  }

  /**
   * Muestra el modal de agregar un registro.
   */
  protected agregar(): void {
    const initialState = {
      accion: FORM_ACTION.Agregar,
      esEdicion: false,
      tipoExamen: this.tipoExamen,
      onClose: (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }

        bsModalRef.hide();
      },
    };

    const bsModalRef = this.modalService.show(
      ContenidoExamenFormularioComponent,
      {
        initialState: initialState,
        ...MODAL_CONFIG.Large,
      }
    );
  }

  /**
   * Muestra el modal de editar un registro y lo carga.
   */

  private editar(idContenidoExamen: number): void {
    const initialState = {
      accion: FORM_ACTION.Editar,
      esEdicion: true,
      idContenidoExamen: idContenidoExamen,
      tipoExamen: this.tipoExamen,
      onClose: (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }

        bsModalRef.hide();
      },
    };

    const bsModalRef = this.modalService.show(
      ContenidoExamenFormularioComponent,
      {
        initialState: initialState,
        ...MODAL_CONFIG.Large,
      }
    );
  }

  private eliminar(contenidoExamen: ContenidoExamen): void {
    const MENSAJE_EXITO: string = 'El contenido del cuestionario ha sido eliminado';
    const TITULO_MODAL: string = 'Eliminar Contenido del cuestionario';
    const MENSAJE_CONFIRMACION: string = `¿Desea eliminar el contenido del Cuestionario: <strong>${contenidoExamen.clave}</strong>?`;

    this.mensajeService
      .modalConfirmacion(
        MENSAJE_CONFIRMACION,
        TITULO_MODAL,
        ICONO.Cruz
      )
      .then(() => {
        this.contenidoExamenService
          .eliminar(contenidoExamen.idContenidoExamen)
          .subscribe(() => {
            this.mensajeService.modalExito(MENSAJE_EXITO);
            this.consultarGrid();
          });
      });
  }

  protected backTo(): void {
    this.router.navigate(['administrador/examen/tipo-examen']);
  }
}
