import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Event } from '@angular/router';
import { ExpedienteColumnaSelectorDTO } from '@dtos/gestion-entidades/expediente-columna-selector-dto';
import { ValoresFueraRangoGridDTO } from '@dtos/gestion-expediente/valores-fuera-rango-grid-dto';
import { EntidadEstructuraTablaValorService } from '@http/gestion-entidad/entidad-estructura-tabla-valor.service';
import { SeccionCampoService } from '@http/gestion-entidad/seccion-campo.service';
import { GeneralConstant } from '@utils/general-constant';
import { ChartData, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { lastValueFrom } from 'rxjs';


@Component({
  selector: 'app-expediente-padecimiento',
  templateUrl: './expediente-padecimiento.component.html',
  styleUrls: ['./expediente-padecimiento.component.scss']
})
export class ExpedientePadecimientoComponent implements OnInit {

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
  

  // Configuraciones Histograma
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;
  public barChartType: ChartType = 'bar'
  public barChartLabels: string[] = ['Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes'];
  public barChartData: ChartData<'bar'> = {
    labels: this.barChartLabels,
    datasets: [
      {
        label: "Niveles de Glucosa",
        data: [5, 3, 1, 3, 4],
        backgroundColor: [
          'rgba(255, 99, 132, 0.2)',
          'rgba(255, 159, 64, 0.2)',
          'rgba(255, 205, 86, 0.2)',
          'rgba(75, 192, 192, 0.2)',
          'rgba(54, 162, 235, 0.2)',
          'rgba(153, 102, 255, 0.2)',
          'rgba(201, 203, 207, 0.2)'
        ],
        // backgroundColor: ["red", "green", "blue"],
        borderColor: [
          'rgb(255, 99, 132)',
          'rgb(255, 159, 64)',
          'rgb(255, 205, 86)',
          'rgb(75, 192, 192)',
          'rgb(54, 162, 235)',
          'rgb(153, 102, 255)',
          'rgb(201, 203, 207)'
        ],
        borderWidth: 1
        // hoverBackgroundColor: ["darkred", "darkgreen", "darkblue"],
      }
    ]
  };

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

  public consultarValoresFueraRango(): void {
    lastValueFrom(this.entidadEstructuraTablaValorService.consultarValoresFueraRango(this.idPadecimiento, this.idUsuario))
      .then((valoresFueraRango: ValoresFueraRangoGridDTO[]) => {
        this.valoresFueraRango = valoresFueraRango;
      }
    );
  }

  public consultarTodasVariables(): void {
    lastValueFrom(this.entidadEstructuraTablaValorService.consultarValoresTodasVariables(this.idPadecimiento, this.idUsuario))
      .then((todasVariables: ValoresFueraRangoGridDTO[]) => {
        this.bitacoraMuestras = todasVariables;
      }
    );
  }

  public consultarSeccionesPadecimiento(): void {
    lastValueFrom(this.seccionCampoService.consultarSeccionesPadecimientos(this.idPadecimiento))
      .then((seccionesPadecimiento: ExpedienteColumnaSelectorDTO[]) => {
        this.variableList = seccionesPadecimiento;
      }
    );
  }

  public onChangeVariable(event: Event): void {
    console.log(event);
  }

  public onFiltroChange(event: any): void {
    console.log(event);
  }
  
}
