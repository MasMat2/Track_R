import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApegoTomaMedicamentoDto } from '@dtos/gestion-expediente/apego-toma-medicamento-dto';
import { ExpedienteTratamientoGridDto } from '@dtos/gestion-expediente/expediente-tratamiento-grid-dto';
import { ExpedienteTratamientoService } from '@http/gestion-expediente/expediente-tratamiento.service';
import { ExpedienteTrackrService } from '@http/seguridad/expediente-trackr.service';
import { EncryptionService } from '@services/encryption.service';
import { ColDef, ICellRendererParams, ValueGetterParams } from 'ag-grid-community';
import * as moment from 'moment';
import { Observable, lastValueFrom } from 'rxjs';
import { first } from 'rxjs/operators';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import { ChartConfiguration, ChartData } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-expediente-tratamiento',
  templateUrl: './expediente-tratamiento.component.html',
  styleUrls: ['./expediente-tratamiento.component.scss']
})
export class ExpedienteTratamientoComponent implements OnInit {

  protected expedienteTratamientoList: ExpedienteTratamientoGridDto[] = [];
  private idUsuario: number;

  //Chart
  protected tratamientos: ApegoTomaMedicamentoDto[] = [];
  private data: number[] = [];
  private etiquetas: string[] = [];
  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;

  private readonly backgroundColor = [
    'rgba(105, 94, 147, 1)',
  ];
  
  private readonly borderColor = [
    'rgba(105, 94, 147, 1)',
  ];

  protected chartPlugins = [ChartDataLabels]
  protected chartType: ChartConfiguration<'bar'>['type'] = 'bar';
  protected chartData: ChartData<'bar'> = {
    labels: this.etiquetas,
    datasets: [
      {
        label: "Apego",
        data: this.data,
        backgroundColor: this.backgroundColor,
        borderColor: this.borderColor,
        barThickness: 20,
        datalabels: {
          align: 'top',
          anchor: 'end',
        }
      },
    ],
  };
  protected chartOptions: ChartConfiguration<'bar'>['options'] = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: { display: false },
      datalabels: {
        color: '#00000',
        font: {
          size: 14,
          weight: 'bold'
        },
        formatter: (value: number) => {
          return value + '%';
        }
      }
    }
  };

    // Grid
  protected readonly HEADER_GRID: string = 'Tratamientos';
  protected tratamientos$: Observable<ExpedienteTratamientoGridDto[]>;
  protected columns: ColDef[] = [
    { headerName: 'Núm.', field: 'idExpedienteTratamiento', minWidth: 50 },
    { headerName: 'Fármaco', field: 'farmaco', minWidth: 150 },
    { headerName: 'Cantidad', field: 'cantidad', minWidth: 50 },
    { headerName: 'Unidad', field: 'unidad', minWidth: 50 },
    { headerName: 'Indicaciones', field: 'indicaciones', minWidth: 150 },
    { headerName: 'Padecimiento', field: 'padecimiento', minWidth: 150 },
    { 
      headerName: 'Días', 
      field: 'dias',  
      /* minWidth: 50,
      cellRenderer: (params: ICellRendererParams) => {
        return moment(params.data.fechaRegistro).format('DD/MM/YYYY');
      },
      valueGetter: (params: ValueGetterParams) => {
        return moment(params.data.fechaRegistro).format('DD/MM/YYYY');
      }, */
     },
    { 
      headerName: 'Horario (h)', 
      field: 'horas', 
      minWidth: 50,
      /* cellRenderer: (params: ICellRendererParams) => {
        return moment(params.data.fechaRegistro, 'HH:mm:ss').format('LT');
      },
      valueGetter: (params: ValueGetterParams) => {
        return moment(params.data.fechaRegistro, 'HH:mm:ss').format('LT');
      }, */
     }
  ];

  constructor(
    private expedienteTratamientoService: ExpedienteTratamientoService,
    private route: ActivatedRoute,
    private encryptionService: EncryptionService,
    private expedienteTrackrService : ExpedienteTrackrService
  ) { }

  public ngOnInit(): void{
    this.obtenerParametrosURL();
  }

  
  private async obtenerParametrosURL(): Promise<void> {
    const queryParams = await lastValueFrom(this.route.queryParams.pipe(first()));
    const params = this.encryptionService.readUrlParams(queryParams);
    this.idUsuario = Number(params.i);
    this.consultarParaGrid();
    this.expedienteTrackrService.apegoTratamientoPorPaciente(this.idUsuario)
    .subscribe((data) => this.actualizarGrafica(data));
  }

  private actualizarGrafica( apegoTratamientoData : ApegoTomaMedicamentoDto[])
  {
    this.tratamientos = apegoTratamientoData;
    this.etiquetas = apegoTratamientoData.map(t => t.padecimientoNombre)
    this.data = apegoTratamientoData.map( t => Math.ceil(t.apego));

    this.chartData.labels = this.etiquetas;
    this.chartData.datasets[0].data = this.data;

    this.chart?.update();
  }

  private consultarParaGrid(): void{
    this.tratamientos$ = this.expedienteTratamientoService.consultarParaGrid(this.idUsuario);
  }
}
