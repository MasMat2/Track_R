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
  protected padecimientosFiltrados: PacientesPorPadecimientoDTO[] = [];

  protected padecimientoFiltroSeleccionadoList: PacientesPorPadecimientoDTO[] = [];
  protected verFiltro: boolean = false;
  protected filtrando: boolean = false;
  protected cantidadPadecimientos: number = 0;
  protected cantidadFiltro: number = 0;


  constructor(
    private padecimientoService: PadecimientoService
  ) { }

  ngOnInit() {
    this.padecimientos$ = this.padecimientoService.consultarPacientesPorPadecimiento();
    this.padecimientos$.subscribe(
      pads => {
        this.cantidadPadecimientos = pads.length;
      }
    )
  }

  protected filtrarPadecimientos(){
    if(this.padecimientoFiltroSeleccionadoList && this.padecimientoFiltroSeleccionadoList.length > 0){
      this.padecimientosFiltrados = this.padecimientoFiltroSeleccionadoList;
      this.cantidadFiltro = this.padecimientosFiltrados.length;
      this.filtrando = true;
    }
    else{
      this.padecimientos$.subscribe(
        pads => {
          this.padecimientosFiltrados = pads;
          this.filtrando = false;
        }
      )
    }
  }

  protected activarFiltro(){
    this.verFiltro = !this.verFiltro;
  }

}
