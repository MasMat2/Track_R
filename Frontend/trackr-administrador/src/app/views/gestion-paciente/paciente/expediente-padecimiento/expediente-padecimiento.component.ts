import { lastValueFrom } from 'rxjs';
import { ExpedientePadecimientoService } from './../../../../shared/http/seguridad/expediente-padecimiento.service';
import { Component, Input, OnInit } from '@angular/core';
import { ExpedientePadecimientoGridDTO } from '@dtos/gestion-expediente/expediente-padecimiento-grid-dto';
import { PadecimientoFueraRangoDTO } from '@dtos/gestion-expediente/padecimiento-fuera-rango-dto';

@Component({
  selector: 'app-expediente-padecimiento',
  templateUrl: './expediente-padecimiento.component.html',
  styleUrls: ['./expediente-padecimiento.component.scss']
})
export class ExpedientePadecimientoComponent implements OnInit {

  @Input() public titulo: string;
  @Input() public idPadecimiento: number;
  @Input() public idUsuario: number;

  public valoresFueraRango: PadecimientoFueraRangoDTO[] = [];

  public columns = [
    { headerName: 'Variable', field: 'variable', minWidth: 150 },
    { headerName: 'Parámetro', field: 'parametro', minWidth: 150 },
    { headerName: 'Fecha & Hora', field: 'fechaHora', minWidth: 150 },
    { headerName: 'Valor Registrado', field: 'valor', minWidth: 150 },
    { headerName: 'Valor de Referencia (min-máx)', field: 'valorReferencia', minWidth: 150 },
  ];
  constructor(
    private expedientePadecimientoService: ExpedientePadecimientoService
  ) { }

  ngOnInit() {
    this.consultarValoresFueraRango();
  }

  public onGridClick(event: any): void {
    console.log(event);
  }

  public consultarValoresFueraRango(): void {
    lastValueFrom(this.expedientePadecimientoService.consultarValoresFueraRango(this.idPadecimiento, this.idUsuario))
      .then((valoresFueraRango: PadecimientoFueraRangoDTO[]) => {
        this.valoresFueraRango = valoresFueraRango;
      }
    );
  }
}
