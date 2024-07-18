import { Component, OnInit } from '@angular/core';
import { ProgramacionExamenService } from '@http/examen/programacion-examen.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Examen } from '@models/examen/examen';
import { ProgramacionExamen } from '@models/examen/programacion-examen';
import { ACCESO_PROGRAMACION_EXAMEN } from '@utils/codigos-acceso/examen.acceso';
import { FORM_ACTION } from '@utils/constants/constants';
import { ICONO } from '@utils/constants/font-awesome-icons';
import { CONFIG_COLUMN_ACTION, GRID_ACTION } from '@utils/constants/grid';
import { MODAL_CONFIG } from '@utils/constants/modal';
import { ColDef, ICellRendererParams, ValueGetterParams } from 'ag-grid-community';
import * as moment from 'moment';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { ProgramacionExamenFormularioComponent } from './programacion-examen-formulario/programacion-examen-formulario.component';
import { ExamenReactivoService } from '../../../shared/http/examen/examen-reactivo.service';
import { GridGeneralService } from '@sharedComponents/grid-general/grid-general.service';
import { utils, writeFile } from 'xlsx';
import { map } from 'rxjs';
import { RespuestasExcelDto } from '@dtos/gestion-examen/respuestas-excel-dto';
import { ExamenReactivoExcelDto } from '@dtos/gestion-examen/examen-reactivo-excel-dto';

@Component({
  selector: 'app-programacionExamen',
  templateUrl: './programacion-examen.component.html',
})
export class ProgramacionExamenComponent implements OnInit {
  public HEADER_GRID = 'Programación Cuestionarios';

  // Accesos
  protected tieneAccesoAgregar: boolean = false;
  protected readonly ACCESO_EDITAR: string = ACCESO_PROGRAMACION_EXAMEN.Editar;
  protected readonly ACCESO_ELIMINAR: string = ACCESO_PROGRAMACION_EXAMEN.Eliminar;

  // Grid
  protected programacionExamenList: ProgramacionExamen[] = [];

  private readonly COLUMNA_DESCARGAR_EXCEL: ColDef = Object.assign(
    {
      action: GRID_ACTION.DescargarExcel,
      cellRendererSelector: (params: ICellRendererParams) => {
        const component = {
          component: 'actionButton',
          params: { disabled: false },
        };
        return component;
      },
      minWidth: 44,
      maxWidth: 44,
    },
    CONFIG_COLUMN_ACTION
  );

  protected columns: ColDef[] = [
    { headerName: 'Clave', field: 'idProgramacionExamen', minWidth: 50, width: 50, },
    { headerName: 'Responsable', field: 'usuarioResponsable', minWidth: 100, width: 100, },
    { headerName: 'Tipo Cuestionario', field: 'tipoExamen', minWidth: 100, width: 100, },
    { headerName: 'Fecha Programada', field: 'fechaExamen', minWidth: 70, width: 70, 
      cellRenderer: (params: ICellRendererParams) => {
        return moment(params.data.fechaExamen).format('DD/MM/YYYY');
      },
      valueGetter: (params: ValueGetterParams) => {
        return moment(params.data.fechaExamen).format('DD/MM/YYYY');
      },
    },
    { headerName: 'Hora Programada', field: 'horaExamen', minWidth: 70, width: 70, 
      cellRenderer: (params: ICellRendererParams) => {
        return moment(params.data.horaExamen, 'HH:mm:ss').format('LT');
      },
      valueGetter: (params: ValueGetterParams) => {
        return moment(params.data.horaExamen, 'HH:mm:ss').format('LT');
      },
    },
    { headerName: 'Duración Total (Min)', field: 'duracion', minWidth: 70, width: 70, },
    { headerName: 'Porcentaje de avance', field: 'porcentajeAvance', minWidth: 70, width: 70, },
    this.COLUMNA_DESCARGAR_EXCEL,
  ];

  constructor(
    private accesoService: AccesoService,
    private mensajeService: MensajeService,
    private modalService: BsModalService,
    private programacionExamenService: ProgramacionExamenService,
    private examenReactivoService: ExamenReactivoService,
    private gridGeneralService: GridGeneralService
  ) {}

  public ngOnInit(): void {
    this.consultarGrid();
    this.consultarAccesoAgregar();
  }

  private consultarAccesoAgregar(): void {
    this.accesoService
      .tieneAcceso(ACCESO_PROGRAMACION_EXAMEN.Agregar)
      .subscribe((data) => {
        this.tieneAccesoAgregar = data;
      });
  }

  /**
   * Consulta la informacion del grid.
   */
  private consultarGrid(): void {
    this.programacionExamenService.consultarGeneral().subscribe((data) => {
      this.programacionExamenList = data;
    });
  }

  /**
   * Evento que se ejecuta al dar clic en algun boton del grid.
   */
  protected onGridClick(gridData: { accion: string; data: ProgramacionExamen }): void {
    if (gridData.accion === GRID_ACTION.Editar) {
      this.editar(gridData.data.idProgramacionExamen);
      //this.consultarExcelRespuestas(gridData.data.idProgramacionExamen);
    }
    else if (gridData.accion === GRID_ACTION.Eliminar) {
      this.eliminar(gridData.data);
    }
    else if (gridData.accion === GRID_ACTION.DescargarExcel) {
      this.consultarExcelRespuestas(gridData.data.idProgramacionExamen);
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
    }

    const bsModalRef = this.modalService.show(
      ProgramacionExamenFormularioComponent,
      {
        initialState: initialState,
        ...MODAL_CONFIG.Large
      }
    );

    // bsModalRef.content.programacionExamen.fechaAlta = new Date();
    // bsModalRef.content.programacionExamen.estatus = true;
  }

  /**
   * Muestra el modal de editar un registro y lo carga.
   */
  private editar(idProgramacionExamen: number): void {
    const initialState = {
      accion: FORM_ACTION.Editar,
      esEdicion: true,
      idProgramacionExamen: idProgramacionExamen,
      onClose: (actualizar: boolean) => {
        if (actualizar) {
          this.consultarGrid();
        }

        bsModalRef.hide();
      }
    };

    const bsModalRef = this.modalService.show(
      ProgramacionExamenFormularioComponent,
      {
        initialState: initialState,
        ...MODAL_CONFIG.Large
      }
    );

    // bsModalRef.content.consultarProyectoPorElementoTecnica();

    // this.examenService
    //   .consultarCalificaciones(idProgramacionExamen)
    //   .subscribe((examenes) => {
    //     const participantes = examenes.map((element) => element.idUsuarioParticipante);

    //     this.programacionExamen.participantes = participantes;
    //     this.examenList = examenes;
    //   });

    // this.programacionExamenService
    //   .consultar(idProgramacionExamen)
    //   .subscribe((programacionExamen) => {
    //     programacionExamen.fechaExamen = new Date(programacionExamen.fechaExamen);
    //     programacionExamen.fechaAlta = new Date(programacionExamen.fechaAlta);
    //     this.programacionExamen = programacionExamen;
    //   });
  }

  private eliminar(programacionExamen: ProgramacionExamen): void {
    const MENSAJE_EXITO: string = 'Programación del Cuestionario eliminada exitosamente.';
    const TITULO_MODAL: string = 'Eliminar Programación de Cuestionario';

    const fecha = moment(programacionExamen.fechaExamen).format('DD/MM/YYYY');
    const hora = moment(programacionExamen.horaExamen, 'HH:mm:ss').format('LT');

    const MENSAJE_CONFIRMACION: string = `¿Desea eliminar la programación del Cuestionario: <strong>${programacionExamen.tipoExamen} (${fecha} ${hora}) </strong>?`;

    this.mensajeService
      .modalConfirmacion(
        MENSAJE_CONFIRMACION,
        TITULO_MODAL,
        ICONO.Cruz
      )
      .then(() => {
        this.programacionExamenService
          .eliminar(programacionExamen.idProgramacionExamen)
          .subscribe(() => {
            this.mensajeService.modalExito(MENSAJE_EXITO);
            this.consultarGrid();
          });
      });
  }

  protected consultarExcelRespuestas(idProgramacionExamen: number){
    this.examenReactivoService.consultarReactivosExamenParaExcel(idProgramacionExamen).subscribe({
      next: (data)=> {
        this.descargarRespuestasExcel(data, idProgramacionExamen);
      }
    });
  }

  private descargarRespuestasExcel(datos: RespuestasExcelDto, clave: number){
    let headers = datos.preguntas;
    const headerSizes = this.gridGeneralService.getHeaderSizes(headers);

    const workbook = utils.book_new();
    const worksheet = utils.json_to_sheet([headers], { skipHeader: true });
    utils.book_append_sheet(workbook, worksheet, 'Sheet1');
    const range = utils.decode_range(worksheet['!ref'] || 'A1');

    datos.respuestas.forEach((res: ExamenReactivoExcelDto[]) => {
      const startRow = range.e.r + 1;
      const respuestasString = res.map((r: ExamenReactivoExcelDto) => (r.respuestaAlumno));

      respuestasString.forEach((string: any, index: any) => {
        const cellAddress = utils.encode_cell({c: index, r: startRow});
        worksheet[cellAddress] = {v: string};
      })
      range.e.r = startRow;
    });

    worksheet['!ref'] = utils.encode_range(range);
    worksheet['!cols'] = headerSizes;

    const hoy = new Date();
    const fileName =
      hoy.getFullYear() +
      ('0' + (hoy.getMonth() + 1)).slice(-2) +
      ('0' + hoy.getDate()).slice(-2) +
      '_' + 'Respuestas Cuestionario' +
      '_' + clave +
      '.' + 
      'xlsx';

    writeFile(workbook, fileName);
  }

  
}
