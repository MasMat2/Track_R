import { Component, OnInit } from '@angular/core';
import { EstadoGridDto } from '@dtos/catalogo/estado-grid-dto';
import { EstadoService } from '@http/catalogo/estado.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { CrudBase } from '@sharedComponents/crud/crud-base/crud-base';
import { ICrudConfig } from '@sharedComponents/crud/crud-base/crud-config';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { ACCESO_ESTADO } from '@utils/codigos-acceso/catalogo.accesos';
import { MODAL_CONFIG } from '@utils/constants/modal';
import { ColDef } from 'ag-grid-community';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Observable, Subject } from 'rxjs';
import { EstadoFormularioComponent } from './estado-formulario/estado-formulario.component';

@Component({
  templateUrl: 'estado.component.html',
})
export class EstadoComponent extends CrudBase<EstadoGridDto> implements OnInit {
  protected readonly HEADER_GRID: string = 'Estados';

  private destroy$: Subject<void> = new Subject<void>();

  public readonly NOMBRE_ENTIDAD: string = "Estado";

  override crudConfig: ICrudConfig =
  {
    nombreEntidad: this.NOMBRE_ENTIDAD,
    generoGramatical: "masc",
    nombrePropiedadId: "idEstado",
    formConfig: {
      ComponenteFormulario: EstadoFormularioComponent,
      modalConfig: MODAL_CONFIG.Default,
      configAgregar: {
          acceso: ACCESO_ESTADO.Agregar,
      },
      configEditar: {
          acceso: ACCESO_ESTADO.Editar,
      }
    },
    configEliminar: {
      acceso: ACCESO_ESTADO.Eliminar,
      elementToString: (estado: EstadoGridDto) => estado.nombre
    }
  };

  // Grid
  protected columns: ColDef[] = [
    { headerName: 'Clave', field: 'clave', minWidth: 150, width: 70 },
    { headerName: 'Nombre', field: 'nombre', minWidth: 150 },
    { headerName: 'País', field: 'nombrePais', minWidth: 150 },
  ];

  constructor(
    private estadoService: EstadoService,
    accesoService: AccesoService,
    modalService: BsModalService,
    mensajeService: MensajeService,
  ) {
    super(
      accesoService,
      modalService,
      mensajeService
    );
  }

  public ngOnInit(): void {
    super.onInit();
  }

  protected override consultarGrid(): Observable<EstadoGridDto[]> {
    return this.estadoService.consultarParaGrid();
  }

  protected override eliminar(idEstado: number): Observable<void> {
    return this.estadoService.eliminar(idEstado);
  }
}
