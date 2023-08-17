import { Component, OnInit } from '@angular/core';
import { ChartConfiguration, ChartData, ChartOptions, ChartType } from 'chart.js';
import ChartDataLabels from 'chartjs-plugin-datalabels';

@Component({
  selector: 'app-grafica-notificaciones',
  templateUrl: './grafica-notificaciones.component.html',
  styleUrls: ['./grafica-notificaciones.component.scss']
})
export class GraficaNotificacionesComponent implements OnInit {

  protected solicitudes: { tipo: string, cantidad: number }[] = [
    { tipo: 'Solicitudes Atendidas', cantidad: 20 },
    { tipo: 'Solicitudes Sin Atender', cantidad: 10 },
  ];

  protected colors: string[] = ['#92d050', '#ff8b8b'];
  protected data = this.solicitudes.map((r) => r.cantidad);
  private etiquetas = this.solicitudes.map((r) => r.tipo);
  private total: number = this.data.reduce((a, b) => a + b, 0);

  protected chartType: ChartConfiguration<'doughnut'>['type'] = 'doughnut';
  protected chartPlugins = [ChartDataLabels];
  protected chartData: ChartData<'doughnut'> = {
    labels: this.etiquetas,
    datasets: [
      {
        label: "Solicitudes",
        data: this.data,
        backgroundColor: this.colors,
      }
    ],
  };
  protected chartOptions: ChartConfiguration<'doughnut'>['options'] = {
    cutout: '80%',
    plugins: {
      legend: { display: false },
      datalabels: {
        color: 'black',
        formatter: (value: number) => {
          return (value / this.total * 100).toFixed(0) + '%';
        }
      }
    }
  };

  constructor() { }

  ngOnInit() {
  }

}
