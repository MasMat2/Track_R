import { Component, OnInit } from '@angular/core';
import { ChartConfiguration, ChartData, ChartOptions, ChartType } from 'chart.js';
import ChartDataLabels from 'chartjs-plugin-datalabels';

@Component({
  selector: 'app-grafica-pacientes',
  templateUrl: './grafica-pacientes.component.html',
  styleUrls: ['./grafica-pacientes.component.scss']
})
export class GraficaPacientesComponent implements OnInit {

  private reporte: { riesgo: string, cantidad: number }[] = [
    { riesgo: 'Bajo Riesgo', cantidad: 20 },
    { riesgo: 'Riesgo Medio', cantidad: 10 },
    { riesgo: 'Riesgo Alto', cantidad: 5 }
  ];

  protected colors: string[] = ['#ff8b8b', '#ffc000', '#92d050'];
  protected data = this.reporte.map((r) => r.cantidad);
  private etiquetas = this.reporte.map((r) => r.riesgo);
  private total: number = this.data.reduce((a, b) => a + b, 0);

  protected chartType: ChartConfiguration<'doughnut'>['type'] = 'doughnut';
  protected chartPlugins = [ChartDataLabels];
  protected chartData: ChartData<'doughnut'> = {
    labels: this.etiquetas,
    datasets: [
      {
        label: "Pacientes",
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
