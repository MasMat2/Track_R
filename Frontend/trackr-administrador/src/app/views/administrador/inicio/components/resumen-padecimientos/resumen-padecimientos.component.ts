import { Component, OnInit } from '@angular/core';
import { PadecimientoService } from '@http/padecimientos/padecimiento.service';
import { Observable, tap } from 'rxjs';
import { PacientesPorPadecimientoDTO } from '@dtos/padecimientos/pacientes-por-padecimiento-dto';

@Component({
  selector: 'app-resumen-padecimientos',
  templateUrl: './resumen-padecimientos.component.html',
  styleUrls: ['./resumen-padecimientos.component.scss']
})
export class ResumenPadecimientosComponent implements OnInit {

  protected padecimientos$: Observable<PacientesPorPadecimientoDTO[]>;

  constructor(
    private padecimientoService: PadecimientoService
  ) { }

  ngOnInit() {
    this.padecimientos$ = this.padecimientoService.consultarPacientesPorPadecimiento();
  }

}
