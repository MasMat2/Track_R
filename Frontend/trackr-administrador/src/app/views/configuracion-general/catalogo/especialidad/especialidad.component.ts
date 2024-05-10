import { Component, OnInit } from '@angular/core';
import { EspecialidadGridDto } from '@dtos/catalogo/especialidad-grid-dto';
import { EspecialidadService } from '@http/catalogo/especialidad.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { CrudBase } from '@sharedComponents/crud/crud-base/crud-base';
import { ICrudConfig } from '@sharedComponents/crud/crud-base/crud-config';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { ACCESO_ESPECIALIDAD} from '@utils/codigos-acceso/catalogo.accesos';
import { MODAL_CONFIG } from '@utils/constants/modal';
import { ColDef } from 'ag-grid-community';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Observable, Subject } from 'rxjs';
import { EspecialidadFormularioComponent } from './especialidad-formulario/especialidad-formulario.component';
import { EspecialidadFormularioCapturaDto } from '@dtos/catalogo/especialidad-formulario-captura-dto';

@Component({
  templateUrl: './especialidad.component.html',
})
export class EspecialidadComponent extends CrudBase<EspecialidadGridDto> implements OnInit {
  protected readonly HEADER_GRID: string = 'Especialidades';
  
  private destroy$: Subject<void> = new Subject<void>();

  public readonly NOMBRE_ENTIDAD: string = "Especialidad";

  override crudConfig: ICrudConfig =
  {
    nombreEntidad: this.NOMBRE_ENTIDAD,
    generoGramatical: "masc",
    nombrePropiedadId: "idEspecialidad",
    formConfig: {
      ComponenteFormulario: EspecialidadFormularioComponent,
      modalConfig: MODAL_CONFIG.Default,

      configAgregar: {
          acceso: ACCESO_ESPECIALIDAD.Agregar,
      },
      configEditar: {
          acceso: ACCESO_ESPECIALIDAD.Editar,
      }
    },
    configEliminar: {
      acceso: ACCESO_ESPECIALIDAD.Eliminar,
      elementToString: (especialidad: EspecialidadGridDto) => especialidad.nombre
    }
  };

  // Grid
  protected columns: ColDef[] = [
    { headerName: 'Clave', field: 'idEspecialidad', maxWidth: 150 },
    { headerName: 'Especialidad', field: 'nombre', minWidth: 600 }
  ];

  constructor(
    private especialidadService: EspecialidadService,
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

  protected override consultarGrid(): Observable<EspecialidadGridDto[]> {
    return this.especialidadService.consultarParaGrid();
  }

  protected override eliminar(idEspecialidad: number): Observable<void> {
    return this.especialidadService.eliminar(idEspecialidad);
  }
}