import { Component, OnInit } from '@angular/core';
import { ChartConfiguration, ChartData } from 'chart.js';
import ChartDataLabels from 'chartjs-plugin-datalabels';

@Component({
  selector: 'app-grafica-tratamientos',
  templateUrl: './grafica-tratamientos.component.html',
  styleUrls: ['./grafica-tratamientos.component.scss']
})
export class GraficaTratamientosComponent implements OnInit {

  private tratamientos: { padecimiento: string, apego: number }[] = [
    { padecimiento: 'Diabetes', apego: 85 },
    { padecimiento: 'Hipertensión', apego: 90 },
    { padecimiento: 'Renal', apego: 75 },
    { padecimiento: 'Obesidad', apego: 40 },
    { padecimiento: 'Enfermedad Coronaria', apego: 65 },
  ];

  private data: number[] = this.tratamientos.map(t => t.apego);
  private etiquetas: string[] = this.tratamientos.map(t => t.padecimiento);
  private colors: string[] = ['#70ad47', '#5b9bd5', '#ffc000', '#a5a5a5', '#ed7d31'];

  protected chartPlugins = [ChartDataLabels]
  protected chartType: ChartConfiguration<'bar'>['type'] = 'bar';
  protected chartData: ChartData<'bar'> = {
    labels: this.etiquetas,
    datasets: [
      {
        label: "Apego",
        data: this.data,
        backgroundColor: this.colors,
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
        color: 'black',
        font: {
          weight: 'bold'
        },
        formatter: (value: number) => {
          return value + '%';
        }
      }
    }
  };

  constructor() { }

  ngOnInit() {
  }

}
