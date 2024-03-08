import { Injectable } from '@angular/core';
import { NotificacionDoctorDTO } from '@dtos/notificaciones/notificacion-doctor-dto';
import { NotificacionHubBase } from './notificacion-hub';

@Injectable({
  providedIn: 'root',
})
export class NotificacionDoctorHubService extends NotificacionHubBase<NotificacionDoctorDTO>{
  constructor() {
    super('hub/notificacion-doctor');
  }
}
