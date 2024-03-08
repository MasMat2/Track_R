import { Injectable } from '@angular/core';
import { NotificacionHubBase } from './notificacion-hub';
import { NotificacionPacienteDTO } from '../Dtos/notificaciones/notificacion-paciente-dto';
import { AuthService } from 'src/app/auth/auth.service';

@Injectable({
  providedIn: 'root',
})
export class NotificacionPacienteHubService extends NotificacionHubBase<NotificacionPacienteDTO> {
   constructor(authService : AuthService) {
    super('hub/notificacion-paciente' , authService); 
  }  
}
