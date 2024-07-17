import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { EntidadEstructuraTablaValorService } from '@http/gestion-expediente/entidad-estructura-tabla-valor.service';
import { SeccionCampoService } from '@http/gestion-entidad/seccion-campo.service';
import { ExpedienteColumnaSelectorDTO } from 'src/app/shared/Dtos/gestion-entidades/expediente-columna-selector-dto';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatChipListboxChange, MatChipsModule} from '@angular/material/chips'
import { NgChartsModule } from 'ng2-charts';
import { ChartData, ChartOptions, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { ColDef, ValueGetterParams } from 'ag-grid-community';
import {format} from 'date-fns'
import { ValoresClaveCampoGridDto } from 'src/app/shared/Dtos/gestion-entidades/valores-clave-campo-grid-dto';
import { addIcons } from 'ionicons';
import { ValoresPorClaveCampo } from 'src/app/shared/Dtos/gestion-expediente/valores-clave-campo';
import { ValoresHistogramaDTO } from '../../../../../shared/Dtos/gestion-entidades/valores-histograma-dto';
import { ModalController } from '@ionic/angular/standalone';
import { BitacoraCompletaComponent } from './bitacora-completa/bitacora-completa.component';
import { EntidadEstructuraService } from '../../../../../shared/http/gestion-entidad/entidad-estructura.service';
import { ExpedientePadecimientoSelectorDTO } from '@dtos/seguridad/expediente-padecimiento-selector-dto';


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
    GridGeneralModule,
    HeaderComponent
  ],
  providers: [
    SeccionCampoService,
    EntidadEstructuraTablaValorService
  ]
})
export class SeguimientoPadecimientoComponent  implements OnInit {

  protected HEADER_GRID = 'Bitacora de muestras';
  private idPadecimiento: string;
  protected padecimiento: ExpedientePadecimientoSelectorDTO;
  protected variableList: ExpedienteColumnaSelectorDTO[];
  protected idSeccionVariable: number;
  protected filtroTiempo: string;
  protected valoresCampo: ValoresClaveCampoGridDto;
  protected valorMin: number | null;
  protected valorMax: number | null;
  protected fechaMin: string;
  protected fechaMax: string;

  protected seleccionadaVariable: boolean = false;
  protected seleccionadoFiltroTiempo: boolean = false;
  protected seleccionVacia: boolean = true;
  protected labelHistograma: string = "";
  protected valoresfiltroTiempo = [
    {
      label: 'Hoy',
      value : 'hoy' 
    },
    {
      label: '1 Semana',
      value : '1 semana' 
    },
    {
      label: '2 Semanas',
      value : '2 semanas' 
    },
    {
      label: '3 Semanas',
      value : '3 semanas' 
    },
    {
      label: '1 Mes',
      value : '1 mes' 
    },
    {
      label: '2 Meses',
      value : '2 meses' 
    },
  ]



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
  // private readonly backgroundColor = [
  //   'rgba(255, 99, 132, 0.8)',
  //   'rgba(255, 159, 64, 0.8)',
  //   'rgba(255, 205, 86, 0.8)',
  //   'rgba(75, 192, 192, 0.8)',
  //   'rgba(54, 162, 235, 0.8)',
  //   'rgba(153, 102, 255, 0.8)',
  //   'rgba(201, 203, 207, 0.8)'
  // ];
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
        borderWidth: 1,
      }
    ],
  };

  public barChartEmptyData: ChartData<'bar'> = {
    labels: this.barChartLabels,
    datasets: [
      {
        data: [],
        backgroundColor: this.backgroundColor,
        borderColor: this.borderColor,
        borderWidth: 1,
      }
    ],
  };
  public barChartOptions:ChartOptions = {plugins: {legend: {display: false}}};

  constructor(
    private seccionCampoService: SeccionCampoService,
    private entidadEstructuraService: EntidadEstructuraService,
    private entidadEstructuraTablaValorService: EntidadEstructuraTablaValorService,
    private route: ActivatedRoute,
    private router: Router,
    private modalController: ModalController
  
    ) { addIcons({
      'chevron-left': 'assets/img/svg/chevron-left.svg',
      'chevron-right': 'assets/img/svg/chevron-right.svg',
      'chevron-down': 'assets/img/svg/chevron-down.svg',
      'chevron-up': 'assets/img/svg/chevron-up.svg',
      'arrow-up': 'assets/img/svg/arrow-up.svg',
      'arrow-down': 'assets/img/svg/arrow-down.svg',
    }) }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.idPadecimiento = params.get('id') ?? '';
      this.consultarPadecimiento();
    });

    this.consultarSeccionesPadecimiento();
    
  }

  protected onChangeVariable(event: Event): void {
    this.seleccionadaVariable = true;
    if(event != null && this.filtroTiempo != null){
      this.seleccionVacia = false;
       this.actualizarDatos(this.idSeccionVariable, this.filtroTiempo); 
    }
  }

  protected onFiltroChange(event: MatChipListboxChange): void {
    if(event == undefined){
      this.seleccionadoFiltroTiempo = false;
      this.asignarSeleccionVacia();
    }
    if(this.idSeccionVariable != null && event != null){
      this.seleccionVacia = false;
      this.seleccionadoFiltroTiempo = true;
      this.actualizarDatos(this.idSeccionVariable, event.toString());
    }
  }

  private actualizarDatos(filtroClave: number, filtroTiempo: string): void {
    lastValueFrom(this.entidadEstructuraTablaValorService.consultarValoresPorClaveCampoUsuarioSesion(filtroClave, filtroTiempo))
      .then((valoresPorClaveCampo) => {
        let data:any = {};
        if(filtroTiempo != "hoy"){
          if(!valoresPorClaveCampo.hasOwnProperty('Do')){
            data['Do'] = []
          }else{
            data['Do'] = valoresPorClaveCampo['Do']
          }
          if(!valoresPorClaveCampo.hasOwnProperty('Lu')){
            data['Lu'] = []
          }else{
            data['Lu'] = valoresPorClaveCampo['Lu']
          }
          if(!valoresPorClaveCampo.hasOwnProperty('Ma')){
            data['Ma'] = []
          }else{
            data['Ma'] = valoresPorClaveCampo['Ma']
          }
          if(!valoresPorClaveCampo.hasOwnProperty('Mi')){
            data['Mi'] = []
          }else{
            data['Mi'] = valoresPorClaveCampo['Mi']
          }
          if(!valoresPorClaveCampo.hasOwnProperty('Ju')){
            data['Ju'] = []
          }else{
            data['Ju'] = valoresPorClaveCampo['Ju']
          }
          if(!valoresPorClaveCampo.hasOwnProperty('Vi')){
            data['Vi'] = []
          }else{
            data['Vi'] = valoresPorClaveCampo['Vi']
          }
          if(!valoresPorClaveCampo.hasOwnProperty('Sa')){
            data['Sa'] = []
          }else{
            data['Sa'] = valoresPorClaveCampo['Sa']
          }
        }
        else{
          data = valoresPorClaveCampo
        }

        // Transforma los datos para usar en la gráfica
        const labels = Object.keys(valoresPorClaveCampo);
        if(labels == null || !(labels.length > 0)){
          this.limpiarHistograma();
          return
        }
      
        const datasets = valoresPorClaveCampo[labels[0]].map((_, index) => {
          return {
            data: Object.keys(data).map(label => data[label][index]?.valor ?? 0),
            backgroundColor: this.backgroundColor[index],
            borderColor: this.borderColor[index],
            borderWidth: 1,
          };
        });

        // Actualiza las etiquetas y datos en la configuración de la gráfica
        this.barChartData = {
          labels: Object.keys(data),
          datasets: datasets,
        };

        // Redibuja la gráfica
        this.chart?.update();
      });

      //Consulta los valores para Grid y Max/Min
      lastValueFrom(this.entidadEstructuraTablaValorService.consultarValoresPorClaveCampoParaGridUsuarioSesion(filtroClave, filtroTiempo))
      .then((response) => {
        this.valoresCampo = response;
        if(response.valores.length == 0)
          this.seleccionVacia = true;
        else
          this.seleccionVacia = false;

        this.labelHistograma = this.obtenerLabelRangoDeFechas( this.filtroTiempo,response.valores);

        //Max
        const { maxValor, fechaMax } = this.valoresCampo.valores.reduce(
          (acc: any, data: any) => (data.valor > acc.maxValor ? { maxValor: data.valor, fechaMax: data.fechaMuestra } : acc),
          { maxValor: -Infinity, fechaMax: null }
        );
        if(maxValor !== -Infinity)
          this.valorMax = maxValor;
        else
          this.valorMax = null;
        this.fechaMax = format(new Date(fechaMax), 'dd/MM/yyyy')

        //Min
        const { minValor, fechaMin } = this.valoresCampo.valores.reduce(
          (acc: any, data: any) => (data.valor < acc.minValor ? { minValor: data.valor, fechaMin: data.fechaMuestra } : acc),
          { minValor: Infinity, fechaMin: null }
        );
        if(minValor !== Infinity)
          this.valorMin = minValor
        else
          this.valorMin = null;
        this.fechaMin = format(new Date(fechaMin), 'dd/MM/yyyy')

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

  private asignarSeleccionVacia(){
    this.seleccionVacia = true;

    this.barChartData = this.barChartEmptyData;
    this.labelHistograma = "";
    this.valorMax = null;
    this.valorMin = null;
    this.chart?.update();
  }
    
  private consultarPadecimiento(){
    this.entidadEstructuraService.consultarPadecimientoPorId(parseInt(this.idPadecimiento)).subscribe({
      next: (data) => {
        console.log(data);
        this.padecimiento = data;
      }
    })
  }

  private consultarSeccionesPadecimiento(): void {
    lastValueFrom(this.seccionCampoService.consultarSeccionesPadecimientos(this.idPadecimiento))
      .then((seccionesPadecimiento: ExpedienteColumnaSelectorDTO[]) => {
        this.variableList = seccionesPadecimiento;
      }
    );
  }

  regresarBtn(){
    this.router.navigate(['home/dashboard']);
  }

  //Para obtener el label de la grafica ("Abril - Mayo") o ("Hoy, 8 de abril 2024")
  obtenerLabelRangoDeFechas(filtroTiempo: string, valores: ValoresHistogramaDTO[]): string {
    const nombresMeses: string[] = [
      "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
      "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
    ];

    if (valores.length === 0) {
        return "";
    }

    if(filtroTiempo == "hoy"){

      const fecha: Date = new Date(valores[0].fechaMuestra);

      const dia: number = fecha.getDate();
      const mes: string = nombresMeses[fecha.getMonth()];
      const año: number = fecha.getFullYear();

      return `Hoy, ${dia} de ${mes} ${año}`;
    }
    else{
      const fechasDate: Date[] = valores.map(obj => new Date(obj.fechaMuestra));
      const fechaMasAntigua: Date = new Date(Math.min(...fechasDate.map(date => date.getTime())));
      const fechaMasReciente: Date = new Date(Math.max(...fechasDate.map(date => date.getTime())));
  
      const mesAnterior: string = nombresMeses[fechaMasAntigua.getMonth()];
      const mesReciente: string = nombresMeses[fechaMasReciente.getMonth()];

      if(mesAnterior == mesReciente)
        return `${mesAnterior}`;

      return `${mesAnterior} - ${mesReciente}`;
    }
  }

  protected async mostrarTodo(){
    const modal = await this.modalController.create({
      component: BitacoraCompletaComponent,
      componentProps: {
        valoresCampo: this.valoresCampo,
        variable: this.variableList.filter(variable => (variable.idSeccionVariable == this.idSeccionVariable)).map(v => v.variable),
        periodo: this.filtroTiempo,
        padecimiento: this.padecimiento.nombre
      }
    });

    modal.present();
  }
}
