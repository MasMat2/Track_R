import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExpedienteTratamientoGridDto } from '@dtos/gestion-expediente/expediente-tratamiento-grid-dto';
import { ExpedienteTratamientoService } from '@http/gestion-expediente/expediente-tratamiento.service';
import { EncryptionService } from '@services/encryption.service';
import { ColDef } from 'ag-grid-community';
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
    { headerName: 'Núm.', field: 'idExpedienteTratamiento', minWidth: 150 },
    { headerName: 'Farmaco', field: 'farmaco', minWidth: 150 },
    { headerName: 'Cantidad', field: 'cantidad', minWidth: 150 },
    { headerName: 'Unidad', field: 'unidad', minWidth: 150 },
    { headerName: 'Indicaciones', field: 'indicaciones', minWidth: 150 },
    { headerName: 'Padecimiento', field: 'padecimiento', minWidth: 150 },
    { headerName: 'Días', field: 'fechaRegistro', minWidth: 150 },
    { headerName: 'Horario (h)', field: 'fechaRegistro', minWidth: 150 }
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
