import { Component, OnInit } from '@angular/core';
import { CrudBase } from '@sharedComponents/crud/crud-base/crud-base';
import { Observable, Subject } from 'rxjs';
import { ClasificacionPreguntaFormularioComponent } from './clasificacion-pregunta-formulario/clasificacion-pregunta-formulario.component';
import { MODAL_CONFIG } from '@utils/constants/modal';
import { ColDef } from 'ag-grid-community';
import { AccesoService } from '@http/seguridad/acceso.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ICrudConfig } from '@sharedComponents/crud/crud-base/crud-config';
import { ACCESO_CLASIFICACION_PREGUNTA } from '@utils/codigos-acceso/examen.acceso';
import { ClasificacionPreguntaGridDto } from 'src/app/shared/examen/clasificacion-pregunta-grid-dto';
import { ClasificacionPreguntaService } from '@http/clasificacion-pregunta.service';

@Component({
  selector: 'app-clasificacion-pregunta',
  templateUrl: './clasificacion-pregunta.component.html',
  styleUrls: ['./clasificacion-pregunta.component.scss']
})
export class ClasificacionPreguntaComponent extends CrudBase<ClasificacionPreguntaGridDto> implements OnInit{
  protected isCollapsed = true;
  
  idClasificacionPregunta: number;

  protected readonly HEADER_GRID: string = 'Clasificación Pregunta';

  private destroy$: Subject<void> = new Subject<void>();

  public readonly NOMBRE_ENTIDAD: string = "Clasificación Pregunta";

  override  crudConfig: ICrudConfig =
    {
      nombreEntidad: this.NOMBRE_ENTIDAD,
      generoGramatical: 'masc',
      nombrePropiedadId: 'idClasificacionPregunta',
      formConfig: {
        ComponenteFormulario: ClasificacionPreguntaFormularioComponent,
        modalConfig: MODAL_CONFIG.Large,
        configAgregar: {
          acceso: ACCESO_CLASIFICACION_PREGUNTA.Agregar,
        },
        configEditar: {
          acceso: ACCESO_CLASIFICACION_PREGUNTA.Editar,
        }
      },
      configEliminar: {
        acceso: ACCESO_CLASIFICACION_PREGUNTA.Eliminar,
        elementToString: (dto: ClasificacionPreguntaGridDto) => dto.clave
      }
    };

  protected columns: ColDef[] = [
    { headerName: 'Clave', field: 'clave', minWidth: 50, width: 100 , valueGetter: (params: any) => (params.node.rowIndex + 1 ).toString() },
    {
      headerName: 'Estatus', field: 'estatus', minWidth: 50, width: 100,
      valueGetter: (params) => params.data.estatus ? 'Activo' : 'Inactivo'
    },
    
    { headerName: 'Clasificación Pregunta', field: 'nombre', minWidth: 50, width: 120 },

  ];

  constructor(
    private clasificacionPreguntaService: ClasificacionPreguntaService,
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
  
  protected override consultarGrid(): Observable<ClasificacionPreguntaGridDto[]> {
    return this.clasificacionPreguntaService.consultarParaGrid();
  }
  protected override eliminar(id: number): Observable<void> {
    return this.clasificacionPreguntaService.eliminar(id);
  }
  

}
