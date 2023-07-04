import { Component, OnInit } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { EncryptionService } from '@services/encryption.service';
import { GeneralConstant } from '@utils/general-constant';
import { ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { format } from 'date-fns';
import { ExpedientePadecimientoGridDTO } from '@dtos/seguridad/expediente-padecimiento-grid-dto';
import { ExpedientePadecimientoService  } from '@http/seguridad/expediente-padecimiento.service';


@Component({
  selector: 'app-expediente-padecimiento',
  templateUrl: './expediente-padecimiento.component.html',
  styleUrls: ['./expediente-padecimiento.component.scss']
})
export class ExpedientePadecimientoComponent implements OnInit {

  protected idUsuario: number;
  protected padecimientoPacienteList: ExpedientePadecimientoGridDTO[] = [];
  public HEADER_GRID = 'Pacientes';

  public columns = [
    { headerName: 'Tipo de Padecimiento', field: 'nombrePadecimiento', minWidth: 150 },
    { headerName: 'Fecha de Diagnóstico', field: 'fechaDiagnostico', minWidth: 150 },
    { headerName: 'Doctor', field: 'nombreDoctor', minWidth: 150 }
  ];


  constructor(
    private route: ActivatedRoute,
    private encryptionService: EncryptionService,
    private expedientePadecimientoService: ExpedientePadecimientoService
  ) { }

  ngOnInit(){
    this.obtenerParametrosURL();
  }


  private async obtenerParametrosURL(): Promise<void> {
    const queryParams = await lastValueFrom(this.route.queryParams.pipe(first()));
    const params = this.encryptionService.readUrlParams(queryParams);
    this.idUsuario = Number(params.i);
    this.consultarPadecimientos();
  }

  private consultarPadecimientos(){
    lastValueFrom(this.expedientePadecimientoService.consultarParaGridPorUsuario(this.idUsuario))
    .then((padecimientoPacienteList: ExpedientePadecimientoGridDTO[]) => {
      this.padecimientoPacienteList = this.formatearFechas(padecimientoPacienteList);
    });

    
  }


  protected formatearFechas(padecimientos: ExpedientePadecimientoGridDTO[]): ExpedientePadecimientoGridDTO[] {
    return padecimientos.map(padecimiento => {

      const fechaDiagnostico = new Date(padecimiento.fechaDiagnostico);
      const fechaFormateada = format(fechaDiagnostico, 'dd/MM/yyyy');
      return { ...padecimiento, fechaDiagnostico: fechaFormateada };
    });
  }

}
