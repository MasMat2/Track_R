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
import { ColDef, ValueGetterParams } from 'ag-grid-community';
import {format} from 'date-fns'


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
  protected filtroTiempo: string = "hoy" ?? "1 semana";
  protected valoresCampo: any;
  protected valorMin: number | null;
  protected valorMax: number | null;
  protected fechaMin: string;
  protected fechaMax: string;


  // Configuración Columnas Data Grid
  protected columns: ColDef[] = [
    { headerName: 'Fecha', field: 'fechaMuestra', minWidth: 15, 
      valueGetter: (params: ValueGetterParams) => {
        const fecha = new Date(params.data.fechaMuestra);
        return format(fecha, 'dd/MM/yyyy');
      }
    },
    { headerName: 'Hora', field: 'fechaMuestra', minWidth: 15,
      valueGetter: (params: ValueGetterParams) => {
        const hora = new Date(params.data.fechaMuestra);
        return format(hora, 'HH:mm')
      }
    },
    { headerName: 'Valor', field: 'valor', minWidth: 15 },
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
      this.actualizarDatos(event.toString(), this.filtroTiempo);
    }
  }

  protected onFiltroChange(event: MatChipListboxChange): void {
    if(this.claveVariable != null && event != null){
      this.actualizarDatos(this.claveVariable, event.toString());
    }
  }

  private actualizarDatos(filtroClave: string, filtroTiempo: string): void {

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

      //Consulta los valores para Grid y Max/Min
      lastValueFrom(this.entidadEstructuraTablaValorService.consultarValoresPorClaveCampoParaGridUsuarioSesion(filtroClave, filtroTiempo))
      .then((response) => {
        this.valoresCampo = response;
        console.log(this.valoresCampo);

        //Max
        const { maxValor, fechaMax } = this.valoresCampo.reduce(
          (acc: any, data: any) => (data.valor > acc.maxValor ? { maxValor: data.valor, fechaMax: data.fechaMuestra } : acc),
          { maxValor: -Infinity, fechaMax: null }
        );
        if(maxValor !== -Infinity)
          this.valorMax = maxValor;
        else
          this.valorMax = null;

        this.fechaMax = format(new Date(fechaMax), 'dd/MM/yyyy')

        //Min
        const { minValor, fechaMin } = this.valoresCampo.reduce(
          (acc: any, data: any) => (data.valor < acc.minValor ? { minValor: data.valor, fechaMin: data.fechaMuestra } : acc),
          { minValor: Infinity, fechaMin: null }
        );
        if(minValor !== Infinity)
          this.valorMin = minValor
        else
          this.valorMin = null;
        
        this.fechaMin = format(new Date(fechaMin), 'dd/MM/yyyy')
        

        

        console.log('This Valor máximo:', this.valorMax);
        console.log('This Fecha de valor maximo:', this.fechaMax);
        console.log('This Valor mínimo:', this.valorMin);
        console.log('This Fecha de valor minimo:', this.fechaMin);
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
