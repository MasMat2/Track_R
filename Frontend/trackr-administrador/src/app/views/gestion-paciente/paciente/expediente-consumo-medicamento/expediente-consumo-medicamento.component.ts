import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExpedienteConsumoMedicamentoGridDto } from '@dtos/gestion-expediente/expediente-consumo-medicamento-grid-dto';
import { ExpedienteConsumoMedicamentoService } from '@http/gestion-expediente/expediente-consumo-medicamento.service';
import { EncryptionService } from '@services/encryption.service';
import { ColDef, ICellRendererParams, ValueGetterParams } from 'ag-grid-community';
import * as moment from 'moment';
import { Observable, lastValueFrom } from 'rxjs';
import { first } from 'rxjs/operators';
import { GeneralConstant } from '@utils/general-constant';

@Component({
  selector: 'app-expediente-consumo-medicamento',
  templateUrl: './expediente-consumo-medicamento.component.html',
  styleUrls: ['./expediente-consumo-medicamento.component.scss']
})
export class ExpedienteConsumoMedicamentoComponent implements OnInit {
  protected consumoMedicamentoList: ExpedienteConsumoMedicamentoGridDto[] = [];
  private idUsuario: number;

    // Grid
  protected readonly HEADER_GRID: string = 'Consumo Medicamentos';
  protected consumoMedicamento$: Observable<ExpedienteConsumoMedicamentoGridDto[]>;
  protected columns: ColDef[] = [
    { headerName: 'Núm.', field: 'idTomaTratamiento', minWidth: 50 },
    { headerName: 'Fármaco', field: 'farmaco', minWidth: 150 },
    { headerName: 'Cantidad', field: 'cantidad', minWidth: 50 },
    { headerName: 'Unidad', field: 'unidad', minWidth: 50 },
    { headerName: 'Padecimiento', field: 'padecimiento', minWidth: 150 },
    { 
      headerName: 'Día ', 
      field: 'fechaToma',  
      minWidth: 50,
      cellRenderer: (params: ICellRendererParams) => {
        return moment(params.data.fechaToma).format('DD/MM/YYYY');
      },
      valueGetter: (params: ValueGetterParams) => {
        return moment(params.data.fechaToma).format('DD/MM/YYYY');
      },
     },
    { 
      headerName: 'Horario (h)', 
      field: 'fechaToma', 
      minWidth: 50,
      cellRenderer: (params: ICellRendererParams) => {
        return moment(params.data.fechaToma, 'HH:mm:ss').format('LT');
      },
      valueGetter: (params: ValueGetterParams) => {
        return moment(params.data.fechaToma, 'HH:mm:ss').format('LT');
      },
     }
  ];

  constructor(
    private expedienteTratamientoService: ExpedienteConsumoMedicamentoService,
    private route: ActivatedRoute,
    private encryptionService: EncryptionService
  ) { }

  public ngOnInit(): void{
    this.obtenerParametrosURL();
  }

  protected consultarParaGrid(): void{
    this.consumoMedicamento$ = this.expedienteTratamientoService.consultarParaGrid(this.idUsuario)
  }

  private async obtenerParametrosURL(): Promise<void> {
    const queryParams = await lastValueFrom(this.route.queryParams.pipe(first()));
    const params = this.encryptionService.readUrlParams(queryParams);
    this.idUsuario = Number(params.i);
    this.consultarParaGrid();
  }

  public onGridClick(gridData: { accion: string; data: ExpedienteConsumoMedicamentoGridDto }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      /** this.editar(gridData.data.idUsuario); **/
      return;
    }
  }
}
