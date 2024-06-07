import { Component, OnInit, ViewChild } from '@angular/core';
import { ApegoTomaMedicamentoDto } from '@dtos/gestion-expediente/apego-toma-medicamento-dto';
import { ExpedienteTrackrService } from '@http/seguridad/expediente-trackr.service';
import { ChartConfiguration, ChartData } from 'chart.js';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-grafica-tratamientos',
  templateUrl: './grafica-tratamientos.component.html',
  styleUrls: ['./grafica-tratamientos.component.scss']
})
export class GraficaTratamientosComponent implements OnInit {


  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;
  protected tratamientos: ApegoTomaMedicamentoDto[] = [];
  private data: number[] = [];
  private etiquetas: string[] = [];
  private colors: string[] = ['#70ad47', '#5b9bd5', '#ffc000', '#a5a5a5', '#ed7d31'];

  private readonly backgroundColor = [
    'rgba(105, 94, 147, 1)',
    'rgba(105, 94, 147, 1)',
    'rgba(105, 94, 147, 1)',
    'rgba(105, 94, 147, 1)',
    'rgba(105, 94, 147, 1)',
    'rgba(105, 94, 147, 1)',
    'rgba(105, 94, 147, 1)',
  ];
  private readonly borderColor = [
    'rgba(105, 94, 147, 1)',
    'rgba(105, 94, 147, 1)',
    'rgba(105, 94, 147, 1)',
    'rgba(105, 94, 147, 1)',
    'rgba(105, 94, 147, 1)',
    'rgba(105, 94, 147, 1)',
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
          align: 'end',
          anchor: 'start',
        }
      },
    ],
  };
  protected chartOptions: ChartConfiguration<'bar'>['options'] = {
    plugins: {
      legend: { display: false },
      datalabels: {
        color: '#525252',
        font: {
          weight: 'bold'
        },
        formatter: (value: number) => {
          return value + '%';
        }
      }
    }
  };

  constructor(
    private expedienteTrackrService : ExpedienteTrackrService
  ) { }

  ngOnInit()
  {
    this.expedienteTrackrService.apegoTratamientos()
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


}
