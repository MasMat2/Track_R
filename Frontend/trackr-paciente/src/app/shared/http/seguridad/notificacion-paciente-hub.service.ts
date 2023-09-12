import { Injectable } from '@angular/core';
import { NotificacionPacienteDTO } from '../../../../../../trackr-administrador/src/app/shared/dtos/notificaciones/notificacion-paciente-dto';
import { NotificacionHubBase } from '../../../../../../trackr-administrador/src/app/shared/services/notificacion-hub';

@Injectable({
  providedIn: 'root',
})
export class NotificacionPacienteHubServiceI extends NotificacionHubBase<NotificacionPacienteDTO> {
  constructor() {
    super('hub/notificacion-paciente');
  }

  async marcaVista(id: number) {
    this.marcarComoVista(id);
  }
}
