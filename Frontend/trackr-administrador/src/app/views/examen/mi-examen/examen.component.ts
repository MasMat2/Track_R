import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ExamenService } from '@http/examen/examen.service';
import { Examen } from '@models/examen/examen';
import { CONFIG_COLUMN_ACTION } from '@utils/constants/grid';
import { ColDef, ICellRendererParams, ValueGetterParams } from 'ag-grid-community';
import * as moment from 'moment';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { EncryptionService } from 'src/app/shared/services/encryption.service';

@Component({
  selector: 'app-reactivo',
  templateUrl: './examen.component.html',
})
export class ExamenComponent implements OnInit {

  protected readonly HEADER_GRID: string = 'Mis Exámenes';

  // Grid
  private readonly GRID_ACCION_PRESENTAR: string = 'play';
  protected examenList: Examen[] = [];

  private columnaPresentar: ColDef = Object.assign(
    {
      action: this.GRID_ACCION_PRESENTAR,
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
    {
      headerName: 'Tipo de Examen',
      field: 'tipoExamen',
      minWidth: 70,
      width: 70,
    },
    {
      headerName: 'Fecha Programada',
      field: 'fechaExamen',
      minWidth: 70,
      width: 70,
      cellRenderer: (params: ICellRendererParams) => {
        return moment(params.data.fechaExamen).format('DD/MM/YYYY');
      },
      valueGetter: (params: ValueGetterParams) => {
        return moment(params.data.fechaExamen).format('DD/MM/YYYY');
      },
    },
    {
      headerName: 'Hora Programada',
      field: 'horaExamen',
      minWidth: 70,
      width: 70,
      cellRenderer: (params: ICellRendererParams) => {
        return moment(params.data.horaExamen, 'HH:mm:ss').format('LT');
      },
      valueGetter: (params: ValueGetterParams) => {
        return moment(params.data.horaExamen, 'HH:mm:ss').format('LT');
      },
    },
    {
      headerName: 'Duración Total (Min)',
      field: 'duracion',
      minWidth: 70,
      width: 70,
    },
    {
      headerName: 'Total de Preguntas',
      field: 'totalPreguntas',
      minWidth: 70,
      width: 70,
    },
    this.columnaPresentar,
  ];

  constructor(
    private encryptionService: EncryptionService,
    private examenService: ExamenService,
    private mensajeService: MensajeService,
    private router: Router
  ) {}

  public ngOnInit(): void {
    this.consultarGrid();
  }

  /**
   * Consulta la informacion del grid.
   */
  public consultarGrid(): void {
    this.examenService
      .consultarMisExamenes()
      .subscribe((examenes: Examen[]) => {
        this.examenList = examenes;
      });
  }

  /**
   * Evento que se ejecuta al dar clic en algun boton del grid.
   */
  public onGridClick(gridData: { accion: string; data: Examen }) {
    if (gridData.accion === this.GRID_ACCION_PRESENTAR) {
      this.presentar(gridData.data.idExamen);
    }
  }

  public presentar(idExamen: number) {
    this.examenService
      .consultarMiExamenIndividual(idExamen)
      .subscribe((examen) => {
        if (!this.esFechaValida(examen)) {
          const MENSAJE_NO_ACCESO: string = 'Aún no tiene acceso a este examen';
          this.mensajeService.modalError(MENSAJE_NO_ACCESO);
          return;
        }

        this.router.navigate(['/administrador/examen/examen/presentar'], {
          queryParams: this.encryptionService.generateURL({
            i: idExamen.toString(),
            revision: '0',
          }),
        });

      });
  }

  private esFechaValida(examen: Examen): boolean {
    const fechaExamen = new Date(examen.fechaExamen);
    fechaExamen.setHours(0, 0, 0, 0);

    const today = new Date();
    today.setHours(0, 0, 0, 0);

    const time = today.getHours() + ':' + today.getMinutes() + ':00';

    if (fechaExamen.getTime() !== today.getTime()) {
      return false;
    }

    const horaExamen = new Date('12/25/2021 ' + examen.horaExamen);
    const currentTime = new Date('12/25/2021 ' + time);

    const milisegundos = horaExamen.getTime() - currentTime.getTime();

    const MS_IN_A_DAY: number = 86400000;
    const MS_IN_AN_HOUR: number = 3600000;
    const MS_IN_A_MINUTE: number = 60000;

    const diferenciaMinutos = Math.round(
      ((milisegundos % MS_IN_A_DAY) % MS_IN_AN_HOUR) / MS_IN_A_MINUTE
    );

    if (diferenciaMinutos > 5 || diferenciaMinutos <= -15) {
      return false;
    }

    return true;
  }
}
