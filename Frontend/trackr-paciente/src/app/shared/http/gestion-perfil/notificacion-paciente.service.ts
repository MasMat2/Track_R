import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NotificacionPacientePopOverDto } from '../../Dtos/perfil/notificacion-paciente-dto';

@Injectable({
    providedIn: 'root'
})
export class NotificacionPacienteService {
    private dataUrl = 'notificacion/';

    constructor(public http: HttpClient) { }

    public consultarPopOverPaciente(): Observable<NotificacionPacientePopOverDto[]> {
        return this.http.get<NotificacionPacientePopOverDto[]>(this.dataUrl + `usuario`);
    }

  /*   public marcarComoVista(idNotificacion : number) : Observable<void>{
        return this.http.post<void>(this.dataUrl + 'leida' , idNotificacion);
    } */
}