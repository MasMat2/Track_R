import { Injectable } from '@angular/core';
import { NotificacionPacienteDTO } from '@dtos/notificaciones/notificacion-paciente-dto';
import { NotificacionHubBase } from './notificacion-hub';

@Injectable({
  providedIn: 'root',
})
export class NotificacionPacienteHubService extends NotificacionHubBase<NotificacionPacienteDTO> {
  constructor() {
    super('hub/notificacion-paciente');
  }
}
