import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExpedienteTratamientoGridDto } from '@dtos/gestion-expediente/expediente-tratamiento-grid-dto';
import { ExpedienteTratamientoService } from '@http/gestion-expediente/expediente-tratamiento.service';
import { EncryptionService } from '@services/encryption.service';
import { ColDef, ICellRendererParams, ValueGetterParams } from 'ag-grid-community';
import * as moment from 'moment';
import { Observable, lastValueFrom } from 'rxjs';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-expediente-tratamiento',
  templateUrl: './expediente-tratamiento.component.html',
  styleUrls: ['./expediente-tratamiento.component.scss']
})
export class ExpedienteTratamientoComponent implements OnInit {

  protected expedienteTratamientoList: ExpedienteTratamientoGridDto[] = [];
  private idUsuario: number;

    // Grid
  protected readonly HEADER_GRID: string = 'Tratamientos';
  protected tratamientos$: Observable<ExpedienteTratamientoGridDto[]>;
  protected columns: ColDef[] = [
    { headerName: 'Núm.', field: 'idExpedienteTratamiento', minWidth: 50 },
    { headerName: 'Fármaco', field: 'farmaco', minWidth: 150 },
    { headerName: 'Cantidad', field: 'cantidad', minWidth: 50 },
    { headerName: 'Unidad', field: 'unidad', minWidth: 50 },
    { headerName: 'Indicaciones', field: 'indicaciones', minWidth: 150 },
    { headerName: 'Padecimiento', field: 'padecimiento', minWidth: 150 },
    { 
      headerName: 'Días', 
      field: 'fechaRegistro',  
      minWidth: 50,
      cellRenderer: (params: ICellRendererParams) => {
        return moment(params.data.fechaRegistro).format('DD/MM/YYYY');
      },
      valueGetter: (params: ValueGetterParams) => {
        return moment(params.data.fechaRegistro).format('DD/MM/YYYY');
      },
     },
    { 
      headerName: 'Horario (h)', 
      field: 'fechaRegistro', 
      minWidth: 50,
      cellRenderer: (params: ICellRendererParams) => {
        return moment(params.data.fechaRegistro, 'HH:mm:ss').format('LT');
      },
      valueGetter: (params: ValueGetterParams) => {
        return moment(params.data.fechaRegistro, 'HH:mm:ss').format('LT');
      },
     }
  ];

  constructor(
    private expedienteTratamientoService: ExpedienteTratamientoService,
    private route: ActivatedRoute,
    private encryptionService: EncryptionService
  ) { }

  public ngOnInit(): void{
    this.obtenerParametrosURL();
  }

  protected consultarParaGrid(): void{
    this.tratamientos$ = this.expedienteTratamientoService.consultarParaGrid(this.idUsuario)
  }

  private async obtenerParametrosURL(): Promise<void> {
    const queryParams = await lastValueFrom(this.route.queryParams.pipe(first()));
    const params = this.encryptionService.readUrlParams(queryParams);
    this.idUsuario = Number(params.i);
    this.consultarParaGrid();
  }

}
