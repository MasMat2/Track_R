import { EntidadEstructuraTablaValorService } from '@http/gestion-entidad/entidad-estructura-tabla-valor.service';
import { lastValueFrom } from 'rxjs';
import { ExpedientePadecimientoService } from './../../../../shared/http/seguridad/expediente-padecimiento.service';
import { Component, Input, OnInit } from '@angular/core';
import { ExpedientePadecimientoGridDTO } from '@dtos/gestion-expediente/expediente-padecimiento-grid-dto';
import { ValoresFueraRangoDTO } from '@dtos/gestion-expediente/valores-fuera-rango-dto';
import { last } from 'lodash';

@Component({
  selector: 'app-expediente-padecimiento',
  templateUrl: './expediente-padecimiento.component.html',
  styleUrls: ['./expediente-padecimiento.component.scss']
})
export class ExpedientePadecimientoComponent implements OnInit {

  @Input() public nombrePadecimiento: string;
  @Input() public idPadecimiento: number;
  @Input() public idUsuario: number;

  protected panelOpenState = true;

  public tituloFueraRango = 'Variables fuera de rango.';
  public tituloBitacoraMuestras = 'Bitácora de Muestras (Todas las Variables)';

  public valoresFueraRango: ValoresFueraRangoDTO[] = [];
  public bitacoraMuestras: ValoresFueraRangoDTO[] = [];

  public columns = [
    { headerName: 'Variable', field: 'variable', minWidth: 150 },
    { headerName: 'Parámetro', field: 'parametro', minWidth: 150 },
    { headerName: 'Fecha & Hora', field: 'fechaHora', minWidth: 150 },
    { headerName: 'Valor Registrado', field: 'valorRegistrado', minWidth: 150 },
    { headerName: 'Valor de Referencia (min-máx)', field: 'valorReferencia', minWidth: 150 },
  ];
  constructor(
    private entidadEstructuraTablaValorService: EntidadEstructuraTablaValorService,
  ) { }

  ngOnInit() {
    this.consultarValoresFueraRango();
    this.consultarTodasVariables();
  }

  public onGridClick(event: any): void {
    console.log(event);
  }

  public consultarValoresFueraRango(): void {
    lastValueFrom(this.entidadEstructuraTablaValorService.consultarValoresFueraRango(this.idPadecimiento, this.idUsuario))
      .then((valoresFueraRango: ValoresFueraRangoDTO[]) => {
        this.valoresFueraRango = valoresFueraRango;
      }
    );
  }

  public consultarTodasVariables(): void {
    lastValueFrom(this.entidadEstructuraTablaValorService.consultarValoresTodasVariables(this.idPadecimiento, this.idUsuario))
      .then((todasVariables: ValoresFueraRangoDTO[]) => {
        this.bitacoraMuestras = todasVariables;
      }
    );
  }
}
