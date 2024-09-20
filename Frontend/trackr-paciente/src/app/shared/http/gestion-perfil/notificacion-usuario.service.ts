import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { NotificacionUsuarioBaseDTO } from "../../Dtos/notificaciones/notificacion-usuario-base-dto";
import { HttpClient } from "@angular/common/http";


@Injectable({
    providedIn: 'root'
})
export class NotificacionUsuarioService {
    private dataUrl = 'notificacionUsuario/';

    constructor(public http: HttpClient) { }

    public consultarNotificacion(idNotificacion : number): Observable<NotificacionUsuarioBaseDTO> {
        return this.http.get<NotificacionUsuarioBaseDTO>(this.dataUrl + `${idNotificacion}`);
    }


}