import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatChipListboxChange } from '@angular/material/chips';
import { Event } from '@angular/router';
import { ExpedienteColumnaSelectorDTO } from '@dtos/gestion-entidades/expediente-columna-selector-dto';
import { ValoresFueraRangoGridDTO } from '@dtos/gestion-expediente/valores-fuera-rango-grid-dto';
import { ValoresHistogramaDTO } from '@dtos/gestion-expediente/valores-histograma-dto';
import { EntidadEstructuraTablaValorService } from '@http/gestion-entidad/entidad-estructura-tabla-valor.service';
import { SeccionCampoService } from '@http/gestion-entidad/seccion-campo.service';
import { GeneralConstant } from '@utils/general-constant';
import { ChartData, ChartOptions, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { last, lastValueFrom } from 'rxjs';


@Component({
  selector: 'app-dashboard-padecimiento',
  templateUrl: './dashboard-padecimiento.component.html',
  styleUrls: ['./dashboard-padecimiento.component.scss']
})
export class DashboardPadecimientoComponent implements OnInit {

  // Inputs
  @Input() public nombrePadecimiento: string;
  @Input() public idPadecimiento: number;
  @Input() public idUsuario: number;

  //Títulos
  public tituloFueraRango = 'Variables fuera de rango.';
  public tituloBitacoraMuestras = 'Bitácora de Muestras (Todas las Variables)';

  // Variables de DataGrid
  public valoresFueraRango: ValoresFueraRangoGridDTO[] = [];
  public bitacoraMuestras: ValoresFueraRangoGridDTO[] = [];

  // Variables
  public claveVariable: string;
  public variableList: ExpedienteColumnaSelectorDTO[] = [];
  protected filtroTiempo: string;

  // Configuracion Columnas DataGrid
  public columns = [
    { headerName: 'Variable', field: 'variable', minWidth: 150 },
    { headerName: 'Parámetro', field: 'parametro', minWidth: 150 },
    { headerName: 'Fecha & Hora', field: 'fechaHora', minWidth: 150 },
    { headerName: 'Valor Registrado', field: 'valorRegistrado', minWidth: 150 },
    { headerName: 'Valor de Referencia (min-máx)', field: 'valorReferencia', minWidth: 150 },
  ];

    // Configuraciones Select
    public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
    public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

    // Colores Histograma

    private readonly backgroundColor = [
      'rgba(255, 99, 132, 0.2)',
      'rgba(255, 159, 64, 0.2)',
      'rgba(255, 205, 86, 0.2)',
      'rgba(75, 192, 192, 0.2)',
      'rgba(54, 162, 235, 0.2)',
      'rgba(153, 102, 255, 0.2)',
      'rgba(201, 203, 207, 0.2)'
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
        // backgroundColor: ["red", "green", "blue"],
        borderColor: this.borderColor,
        borderWidth: 1
        // hoverBackgroundColor: ["darkred", "darkgreen", "darkblue"],
      }
    ],
  };
  public barChartOptions:ChartOptions = {plugins: {legend: {display: false}}};

  constructor(
    private entidadEstructuraTablaValorService: EntidadEstructuraTablaValorService,
    private seccionCampoService: SeccionCampoService,
  ) { 
  }

  ngOnInit() {
    this.consultarValoresFueraRango();
    this.consultarTodasVariables();
    this.consultarSeccionesPadecimiento();
  }

  public onGridClick(event: any): void {
    console.log(event);
  }

  private consultarValoresFueraRango(): void {
    lastValueFrom(this.entidadEstructuraTablaValorService.consultarValoresFueraRango(this.idPadecimiento, this.idUsuario))
      .then((valoresFueraRango: ValoresFueraRangoGridDTO[]) => {
        this.valoresFueraRango = valoresFueraRango;
      }
    );
  }

  private consultarTodasVariables(): void {
    lastValueFrom(this.entidadEstructuraTablaValorService.consultarValoresTodasVariables(this.idPadecimiento, this.idUsuario))
      .then((todasVariables: ValoresFueraRangoGridDTO[]) => {
        this.bitacoraMuestras = todasVariables;
      }
    );
  }

  private consultarSeccionesPadecimiento(): void {
    lastValueFrom(this.seccionCampoService.consultarSeccionesPadecimientos(this.idPadecimiento))
      .then((seccionesPadecimiento: ExpedienteColumnaSelectorDTO[]) => {
        this.variableList = seccionesPadecimiento;
      }
    );
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
    lastValueFrom(this.entidadEstructuraTablaValorService.consultarValoresPorClaveCampo(filtroClave, this.idUsuario, filtroTiempo))
      .then((valoresPorClaveCampo) => {
        // Transforma los datos para usar en la gráfica
      const labels = Object.keys(valoresPorClaveCampo);
      if(labels == null || !(labels.length > 0)){
        this.limpiarHistograma();
        return
      }
      const datasets = valoresPorClaveCampo[labels[0]].map((_, index) => {
        return {
          // labels: labels.map(label => valoresPorClaveCampo[label][index]?.fechaMuestra ?? 'Sin fecha'),
          data: labels.map(label => valoresPorClaveCampo[label][index]?.valor ?? 0),
          backgroundColor: this.backgroundColor[index],
          borderColor: this.borderColor[index],
          borderWidth: 1,
          // label: this.variableList.find(variable => variable.clave == this.claveVariable)?.variable ?? 'Sin nombre',
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
    }

    private limpiarHistograma(): void {
      // Actualiza las etiquetas y datos en la configuración de la gráfica
      this.barChartData.datasets.forEach(dataset => {
        dataset.data.pop();
      });
      this.chart?.update();
    }
  
  
}
