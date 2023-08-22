import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NotificacionDoctorCapturaDTO } from '@dtos/notificaciones/notificacion-doctor-captura-dto';

@Injectable({
  providedIn: 'root'
})
export class NotificacionService {
  private readonly endpoint = 'notificacion/';

  constructor(private http: HttpClient) { }

  public notificar(notificacionDoctorDto: NotificacionDoctorCapturaDTO): Observable<void> {
    return this.http.post<void>(this.endpoint, notificacionDoctorDto);
  }

}
