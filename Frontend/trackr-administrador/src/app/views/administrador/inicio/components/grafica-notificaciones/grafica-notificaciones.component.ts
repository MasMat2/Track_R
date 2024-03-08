import { Component, OnInit, ViewChild } from '@angular/core';
import { NotificacionDoctorDTO } from '@dtos/notificaciones/notificacion-doctor-dto';
import { NotificacionDoctorHubService } from '@services/notificacion-doctor-hub.service';
import { ChartConfiguration, ChartData } from 'chart.js';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-grafica-notificaciones',
  templateUrl: './grafica-notificaciones.component.html',
  styleUrls: ['./grafica-notificaciones.component.scss']
})
export class GraficaNotificacionesComponent implements OnInit {

  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;

  protected colors: string[] = ['#92d050', '#ff8b8b'];
  protected etiquetas: string[] = ['Solicitudes Atendidas', 'Solicitudes Sin Atender'];
  protected data: number[] = [0, 0];
  private total: number = 0;

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
          if (this.total === 0) return (0).toFixed(0) + '%';
          return (value / this.total * 100).toFixed(0) + '%';
        }
      }
    }
  };

  constructor(
    private notificacionHubService: NotificacionDoctorHubService
  ) { }

  ngOnInit() {
    this.notificacionHubService.notificaciones$
      .subscribe((notificaciones) => this.actualizarGrafica(notificaciones));
  }

  private actualizarGrafica(notificaciones: NotificacionDoctorDTO[]) {
    const atendidas = notificaciones.filter((n) => n.visto).length;
    const sinAtender = notificaciones.length - atendidas;

    this.data = [atendidas, sinAtender];
    this.total = this.data.reduce((a, b) => a + b, 0);

    this.chartData.datasets[0].data = this.data;
    this.chart?.update();
  }

}
