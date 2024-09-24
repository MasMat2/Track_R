import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ExamenService } from '@http/examen/examen.service';
import { Examen } from '@models/examen/examen';
import { CONFIG_COLUMN_ACTION, GRID_ACTION } from '@utils/constants/grid';
import { ColDef, ICellRendererParams, ValueGetterParams } from 'ag-grid-community';
import * as moment from 'moment';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { EncryptionService } from 'src/app/shared/services/encryption.service';
import * as Utileria from '@utils/utileria'

@Component({
  selector: 'app-reactivo',
  templateUrl: './examen.component.html',
  styleUrls: ['./examen.component.scss'],
})
export class ExamenComponent implements OnInit {

  protected readonly HEADER_GRID: string = 'Mis Cuestionarios';

  // Grid
  protected examenPendienteList: Examen[] = [];
  protected examenContestadoList: Examen[] = [];
  protected examenVencidoList: Examen[] = [];


  private columnaRevisar: ColDef = Object.assign(
    {
      action: GRID_ACTION.Revisar,
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

  private columnaDescargarPdf: ColDef = Object.assign(
    {
      action: GRID_ACTION.DescargarPdf,
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

  protected columnasConRevisar: ColDef[] = [
    {
      headerName: 'Clave',
      field: 'idExamen',
      minWidth: 70,
      width: 70,
    },
    {
      headerName: 'Participante',
      field: 'nombreUsuario',
      minWidth: 70,
      width: 70,
    },
    {
      headerName: 'Resultado',
      field: 'resultado',
      minWidth: 70,
      width: 70,
    },
    {
      headerName: 'Preguntas correctas',
      field: 'preguntasCorrectas',
      minWidth: 70,
      width: 70,
    },
    this.columnaRevisar,
    this.columnaDescargarPdf,
  ];

  protected columnasSinRevisar: ColDef[] = [
    {
      headerName: 'Clave',
      field: 'idExamen',
      minWidth: 70,
      width: 70,
    },
    {
      headerName: 'Participante',
      field: 'nombreUsuario',
      minWidth: 70,
      width: 70,
    },
    {
      headerName: 'Resultado',
      field: 'resultado',
      minWidth: 70,
      width: 70,
    },
    {
      headerName: 'Preguntas correctas',
      field: 'preguntasCorrectas',
      minWidth: 70,
      width: 70,
    },
    this.columnaDescargarPdf,
  ];

  constructor(
    private encryptionService: EncryptionService,
    private examenService: ExamenService,
    private router: Router
  ) {}

  public ngOnInit(): void {
    this.consultarGridContestados();
    this.consultarGridPendientes();
    this.consultarGridVencidos();
  }

  public consultarGridPendientes(): void {
    this.examenService
      .consultarExamenesPendientesAsignados()
      .subscribe((examenes: Examen[]) => {
        this.examenPendienteList = examenes;
      });
  }

  public consultarGridContestados(): void {
    this.examenService
      .consultarExamenesContestadosAsignados()
      .subscribe((examenes: Examen[]) => {
        this.examenContestadoList = examenes;
      });
  }

  public consultarGridVencidos(): void {
    this.examenService
      .consultarExamenesVencidosAsignados()
      .subscribe((examenes: Examen[]) => {
        this.examenVencidoList = examenes;
      });
  }

  /**
   * Evento que se ejecuta al dar clic en algun boton del grid.
   */
  public onGridClick(gridData: { accion: string; data: Examen }) {
    if (gridData.accion === GRID_ACTION.Revisar) {
      this.revisar(gridData.data.idExamen);
    }
    if(gridData.accion === GRID_ACTION.DescargarPdf){
      this.descargarRespuestasPdf(
        gridData.data.idExamen,
        gridData.data.clave,
        gridData.data.nombreUsuario
      );
    }
  }

  public revisar(idExamen: number) {
    this.router.navigate(['/administrador/examen/examen/presentar'], {
      queryParams: this.encryptionService.generateURL({
        i: idExamen.toString(),
        revision: '1',
      }),
    });
  }

  private descargarRespuestasPdf(idExamen: number, claveExamen: string, nombreUsuario: string){
    this.examenService.descargarRespuestasPDF(idExamen).subscribe(
      (data) => {
        const a = document.createElement('a');
        const objectUrl = URL.createObjectURL(data);
        a.href = objectUrl;
        a.download = Utileria.obtenerFormatoNombreArchivo(
          'Respuestas_' + claveExamen + '_' + nombreUsuario,
          'pdf'
        );
        a.click();

        URL.revokeObjectURL(objectUrl);
      });
  }
}
