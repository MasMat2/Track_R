import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { EntidadEstructuraTablaValorService } from '@http/gestion-expediente/entidad-estructura-tabla-valor.service';
import { SeccionCampoService } from '@http/gestion-expediente/seccion-campo.service';
import { ExpedienteColumnaSelectorDTO } from 'src/app/shared/Dtos/gestion-entidades/expediente-columna-selector-dto';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatChipListboxChange, MatChipsModule} from '@angular/material/chips'
import { NgChartsModule } from 'ng2-charts';
import { ChartData, ChartOptions, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { ValoresFueraRangoGridDTO } from '@dtos/gestion-expediente/valores-fuera-rango-grid-dto';
import { ValoresPorClaveCampo } from 'src/app/shared/Dtos/gestion-expediente/valores-clave-campo';
import { GridGeneralComponent } from '@sharedComponents/grid-general/grid-general.component';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';


@Component({
  selector: 'app-seguimiento-padecimiento',
  templateUrl: './seguimiento-padecimiento.component.html',
  styleUrls: ['./seguimiento-padecimiento.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    IonicModule,
    HeaderComponent,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    NgChartsModule,
    MatChipsModule,
    GridGeneralModule
  ],
  providers: [
    SeccionCampoService,
    EntidadEstructuraTablaValorService
  ]
})
export class SeguimientoPadecimientoComponent  implements OnInit {

  protected HEADER_GRID = 'Bitacora de muestras';
  private idPadecimiento: string;
  protected variableList: ExpedienteColumnaSelectorDTO[];
  protected claveVariable: string;
  protected nombreVariable: string;
  protected filtroTiempo: string = "hoy";
  protected valoresCampo: any

  // Configuración Columnas Data Grid
  protected columns = [
    { headerName: 'Fecha', field: 'fechaMuestra', minWidth: 10 },
    { headerName: 'Valor', field: 'valor', minWidth: 10 },
  ];


  //Configuracion colores para histograma
  private readonly backgroundColor = [
    'rgba(255, 99, 132, 0.8)',
    'rgba(255, 159, 64, 0.8)',
    'rgba(255, 205, 86, 0.8)',
    'rgba(75, 192, 192, 0.8)',
    'rgba(54, 162, 235, 0.8)',
    'rgba(153, 102, 255, 0.8)',
    'rgba(201, 203, 207, 0.8)'
  ];
  private readonly borderColor = [
    'rgb(255, 99, 132)',
    'rgb(255, 159, 64)',
    'rgb(255, 205, 86)',
    'rgb(75, 192, 192)',
    'rgb(54, 162, 235)',
    'rgb(153, 102, 255)',
    'rgb(201, 203, 207)'
  ];

  // Configuraciones Histograma
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;
  public barChartType: ChartType = 'bar'
  public barChartLabels: string[] = ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'];
  public barChartData: ChartData<'bar'> = {
    labels: this.barChartLabels,
    datasets: [
      {
        label: "Niveles de Glucosa",
        data: [],
        backgroundColor: this.backgroundColor,
        borderColor: this.borderColor,
        borderWidth: 1
      }
    ],
  };
  public barChartOptions:ChartOptions = {plugins: {legend: {display: false}}};


  constructor(
    private seccionCampoService: SeccionCampoService,
    private entidadEstructuraTablaValorService: EntidadEstructuraTablaValorService,
    private route: ActivatedRoute
  
    ) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.idPadecimiento = params.get('id') ?? '';
    });

    this.consultarSeccionesPadecimiento();
    
  }

  protected onChangeVariable(event: Event): void {
    if(event != null && this.filtroTiempo != null){
      this.actualizarHistograma(event.toString(), this.filtroTiempo);
    }
  }

  protected onFiltroChange(event: MatChipListboxChange): void {
    if(this.claveVariable != null && event != null){
      this.actualizarHistograma(this.claveVariable, event.toString());
    }
  }

  private actualizarHistograma(filtroClave: string, filtroTiempo: string): void {
    lastValueFrom(this.entidadEstructuraTablaValorService.consultarValoresPorClaveCampoUsuarioSesion(filtroClave, filtroTiempo))
      .then((valoresPorClaveCampo) => {
        // Transforma los datos para usar en la gráfica
      const labels = Object.keys(valoresPorClaveCampo);
      if(labels == null || !(labels.length > 0)){
        this.limpiarHistograma();
        return
      }
      const datasets = valoresPorClaveCampo[labels[0]].map((_, index) => {
        return {
          data: labels.map(label => valoresPorClaveCampo[label][index]?.valor ?? 0),
          backgroundColor: this.backgroundColor[index],
          borderColor: this.borderColor[index],
          borderWidth: 1,
        };
      });

      // Actualiza las etiquetas y datos en la configuración de la gráfica
      this.barChartData = {
        labels: labels,
        datasets: datasets,
      };

      // Redibuja la gráfica
      this.chart?.update();
      });

      lastValueFrom(this.entidadEstructuraTablaValorService.consultarValoresPorClaveCampoParaGridUsuarioSesion(filtroClave, filtroTiempo))
      .then((response) => {
        this.valoresCampo = response;
      }
    );
  }

  private limpiarHistograma(): void {
    // Actualiza las etiquetas y datos en la configuración de la gráfica
    this.barChartData.datasets.forEach(dataset => {
      dataset.data.pop();
    });
    this.chart?.update();
  }
    
  private consultarSeccionesPadecimiento(): void {
    lastValueFrom(this.seccionCampoService.consultarSeccionesPadecimientos(this.idPadecimiento))
      .then((seccionesPadecimiento: ExpedienteColumnaSelectorDTO[]) => {
        this.variableList = seccionesPadecimiento;
      }
    );
  }


}
