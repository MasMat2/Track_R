import { Component, OnInit } from '@angular/core';
import { EstadoGridDto } from '@dtos/catalogo/estado-grid-dto';
import { EstadoService } from '@http/catalogo/estado.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { ACCESO_ESTADO } from '@utils/codigos-acceso/catalogo.accesos';
import { ColDef } from 'ag-grid-community';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Observable, Subject, takeUntil } from 'rxjs';
import { EstadoFormularioComponent } from './estado-formulario/estado-formulario.component';
import { GRID_ACTION } from '@utils/constants/grid';
import { FORM_ACTION } from '@utils/constants/constants';
import { MODAL_CONFIG } from '@utils/constants/modal';

@Component({
  templateUrl: 'estado.component.html',
})
export class EstadoComponent implements OnInit {
  protected readonly HEADER_GRID: string = 'Estados';

  private destroy$: Subject<void> = new Subject<void>();

  // Accesos
  protected readonly ACCESO_EDITAR: string = ACCESO_ESTADO.Editar;
  protected readonly ACCESO_ELIMINAR: string = ACCESO_ESTADO.Eliminar;
  protected tieneAccesoAgregar$: Observable<boolean>;

  // Grid
  protected estados$: Observable<EstadoGridDto[]>;
  protected columns: ColDef[] = [
    { headerName: 'Clave', field: 'clave', minWidth: 150, width: 70 },
    { headerName: 'Nombre', field: 'nombre', minWidth: 150 },
    { headerName: 'País', field: 'nombrePais', minWidth: 150 },
  ];

  constructor(
    private mensajeService: MensajeService,
    private estadoService: EstadoService,
    private accesoService: AccesoService,
    private modalService: BsModalService
  ) {}

  public ngOnInit(): void {
    this.consultarGrid();
    this.consultarAccesoAgregar();
  }

  public consultarAccesoAgregar(): void {
    this.tieneAccesoAgregar$ = this.accesoService.tieneAcceso(ACCESO_ESTADO.Agregar);
  }

  public consultarGrid(): void {
    this.estados$ = this.estadoService.consultarParaGrid();
  }

  public onGridClick(gridData: { accion: string; data: EstadoGridDto }): void {
    const estado = gridData.data;

    const acciones = {
      [GRID_ACTION.Editar as string]: () => this.editar(estado.idEstado),
      [GRID_ACTION.Eliminar as string]: () => this.eliminar(estado),
    };

    acciones[gridData.accion]();
  }

  public agregar(): void {
    const initialState = {
      accion: FORM_ACTION.Agregar,
    };

    const bsModalRef = this.modalService.show(EstadoFormularioComponent, {
      initialState,
      ...MODAL_CONFIG.Default,
    });

    const content = bsModalRef.content as EstadoFormularioComponent;

    const subscription = content.closed
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (estado) => {
          if (estado !== undefined) {
            this.consultarGrid();
          }

          bsModalRef.hide();
        },
        error: (err) => {
        },
        complete: () => {
          subscription.unsubscribe();
        }
      });
  }

  public editar(idEstado: number): void {
    const initialState = {
      idEstado: idEstado,
      accion: FORM_ACTION.Editar,
    };

    const bsModalRef = this.modalService.show(EstadoFormularioComponent, {
      initialState,
      ...MODAL_CONFIG.Default,
    });

    const content = bsModalRef.content as EstadoFormularioComponent;

    const subscription = content.closed
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (estado) => {
          if (estado !== undefined) {
            this.consultarGrid();
          }

          bsModalRef.hide();
        },
        error: (err) => {
        },
        complete: () => {
          subscription.unsubscribe();
        }
      });
  }

  public eliminar(estado: EstadoGridDto): void {
    const TITULO_MODAL: string = 'Eliminar Estado';
    const MENSAJE_CONFIRMACION: string = '¿Desea eliminar el estado <strong>' + estado.nombre + '</strong>?';
    const MENSAJE_EXITO: string = 'El estado ha sido eliminado';

    this.mensajeService
      .modalConfirmacion(
        MENSAJE_CONFIRMACION,
        TITULO_MODAL
      )
      .then((aceptar) => {
        this.estadoService.eliminar(estado.idEstado).subscribe((data) => {
          this.mensajeService.modalExito(MENSAJE_EXITO);
          this.consultarGrid();
        });
      });
  }
}
