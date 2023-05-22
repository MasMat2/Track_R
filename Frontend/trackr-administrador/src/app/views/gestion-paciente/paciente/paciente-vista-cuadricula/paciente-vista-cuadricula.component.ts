import { GeneralConstant } from './../../../../shared/utils/general-constant';
import { Paciente } from './../paciente';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-paciente-vista-cuadricula',
  templateUrl: './paciente-vista-cuadricula.component.html',
  styleUrls: ['./paciente-vista-cuadricula.component.scss']
})
export class PacienteVistaCuadriculaComponent implements OnInit {

  @Input() pacientes: Paciente[] = [];
  @Output() ver = new EventEmitter<any>();
  constructor() { }

  ngOnInit() {
    
  }

  protected descargarExcel(): void {
  }

  callParent(): void {
    
  }

  protected onGridClick(event: Event): void {
    let accion = GeneralConstant.GRID_ACCION_VER;
    let paciente = event;
    let vers = {accion, paciente};
    this.ver.emit(vers);
  }

  protected onVer(event: Paciente): void {
    this.ver.emit(event);
  }

  protected onEditar(event: Event): void {
    this.ver.emit(event);
    
  }

  protected onEliminar(event: Event): void {
  }


}
