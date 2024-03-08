import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ExpedienteEstudioGridDTO } from '@dtos/gestion-expediente/expediente-recomendacion/expediente-estudio-grid-dto';
import { ExpedienteEstudio } from '@models/gestion-expediente/expediente-estudio';

@Injectable({
    providedIn: 'root'
})
export class ExpedienteEstudioService {
    private dataUrl = 'expedienteEstudio/';

    constructor(public http: HttpClient) {}

    consultar(idExpedienteEstudio: number) {
        return this.http.get<ExpedienteEstudio>(this.dataUrl + `consultar/${idExpedienteEstudio}`);
    }

    consultarPorUsuario(idUsuario: number) {
        return this.http.get<ExpedienteEstudioGridDTO[]>(this.dataUrl + `consultarPorUsuario/${idUsuario}`);
    }

    agregar() {
        return this.http.post<number>(this.dataUrl + 'agregar', {});
    }
    
}