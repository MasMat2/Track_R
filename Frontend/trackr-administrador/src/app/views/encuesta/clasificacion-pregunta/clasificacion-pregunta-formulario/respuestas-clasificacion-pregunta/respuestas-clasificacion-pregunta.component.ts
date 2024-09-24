import { Component, Input, OnInit } from '@angular/core';
import { CrudBase } from '@sharedComponents/crud/crud-base/crud-base';
import { RespuestasClasificacionPreguntaFormularioComponent } from './respuestas-clasificacion-pregunta-formulario/respuestas-clasificacion-pregunta-formulario.component';
import { Observable, Subject } from 'rxjs';
import { MODAL_CONFIG } from '@utils/constants/modal';
import { ICrudConfig } from '@sharedComponents/crud/crud-base/crud-config';
import { ACCESO_RESPUESTA_CLASPREGUNTA } from '@utils/codigos-acceso/examen.acceso';
import { ColDef } from 'ag-grid-community';
import { AccesoService } from '@http/seguridad/acceso.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { RespuestasClasificacionPreguntaGridDto } from 'src/app/shared/examen/respuestas-clasificacion-pregunta-grid-dto';
import { RespuestasClasificacionPreguntaService } from '@http/examen/respuestas-clasificacion-pregunta.service';

@Component({
  selector: 'app-respuestas-clasificacion-pregunta',
  templateUrl: './respuestas-clasificacion-pregunta.component.html',
  styleUrls: ['./respuestas-clasificacion-pregunta.component.scss']
})
export class RespuestasClasificacionPreguntaComponent extends CrudBase<RespuestasClasificacionPreguntaGridDto> implements OnInit{
  @Input() idClasificacionPregunta: number;
  protected isCollapsed = true;
  protected readonly HEADER_GRID: string = 'Respuesta';
  protected readonly NOMBRE_ENTIDAD: string = 'Respuesta';
  private destroy$: Subject<void> = new Subject<void>();

  override crudConfig: ICrudConfig = {
    nombreEntidad: this.NOMBRE_ENTIDAD,
    generoGramatical: 'masc',
    nombrePropiedadId: 'idRespuestasClasificacionPregunta',
    formConfig: {
      ComponenteFormulario: RespuestasClasificacionPreguntaFormularioComponent,
      modalConfig: MODAL_CONFIG.Large,
        configAgregar: {
          acceso: ACCESO_RESPUESTA_CLASPREGUNTA.Agregar,
          params: { idClasificacionPregunta: 0}
        },
        configEditar: {
          acceso: ACCESO_RESPUESTA_CLASPREGUNTA.Editar,
        }
      },
    configEliminar: {
      acceso: ACCESO_RESPUESTA_CLASPREGUNTA.Eliminar,
      elementToString: (dto: RespuestasClasificacionPreguntaGridDto) =>
        `${dto.idRespuestasClasificacionPregunta}`,
    },
  };

  protected columns: ColDef[] = [
    {
      headerName: 'Identificador',
      field: 'identificador',
      minWidth: 50,
      width: 120,
    },
    {
      headerName: 'Respuesta',
      field: 'nombre',
      autoHeight: true,
      minWidth: 250,
      width: 300,
    },{
      headerName: 'Valor',
      field: 'valor',
      autoHeight: true,
      minWidth: 250,
      width: 300,
    },
    {
      headerName: 'Estatus', field: 'estatus', minWidth: 50, width: 100,
      valueGetter: (params) => params.data.estatus ? 'Activo' : 'Inactivo'
    },
  ];

  constructor(
    private respuestasClasificacionPreguntaService: RespuestasClasificacionPreguntaService,
    accesoService: AccesoService,
    modalService: BsModalService,
    mensajeService: MensajeService
  ) {
    super(accesoService, modalService, mensajeService);
  }

  public ngOnInit(): void {
    this.cargarDatos(this.idClasificacionPregunta);
  }

 
  protected override consultarGrid(): Observable<RespuestasClasificacionPreguntaGridDto[]> {
    return this.respuestasClasificacionPreguntaService.consultarParaGrid(this.idClasificacionPregunta);
  }
  protected override eliminar(id: number): Observable<void> {
    throw new Error('Method not implemented.');
  }
  cargarDatos(idClasificacionPregunta: number) {
    this.idClasificacionPregunta = idClasificacionPregunta;
    if (this.crudConfig?.formConfig?.configAgregar?.params) {
      this.crudConfig.formConfig.configAgregar.params["idClasificacionPregunta"] =
        this.idClasificacionPregunta;
    }
    super.onInit();
  }

}
