import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UsuarioExpedienteGridDTO } from '@dtos/seguridad/usuario-expediente-grid-dto';
import { GeneralConstant } from '@utils/general-constant';

@Component({
  selector: 'app-paciente-vista-cuadricula',
  templateUrl: './paciente-vista-cuadricula.component.html',
  styleUrls: ['./paciente-vista-cuadricula.component.scss']
})
export class PacienteVistaCuadriculaComponent implements OnInit {

  @Input() pacientes: UsuarioExpedienteGridDTO[] = [];
  @Output() event = new EventEmitter<{ accion: string; data: any }>();
  constructor() { }

  ngOnInit() {
    
  }

  protected descargarExcel(): void {
  }

  callParent(): void {
    
  }

  protected onVer(data:UsuarioExpedienteGridDTO): void {
    const accion = GeneralConstant.GRID_ACCION_VER;
    this.event.emit({accion, data});
  }

  protected onEditar(data: UsuarioExpedienteGridDTO): void {
    const accion = GeneralConstant.GRID_ACCION_EDITAR;
    this.event.emit({accion, data});
    
  }

  protected onEliminar(data: UsuarioExpedienteGridDTO): void {
    const accion = GeneralConstant.GRID_ACCION_ELIMINAR;
    this.event.emit({accion, data});
  }

}
