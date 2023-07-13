import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ExpedienteTratamientoGridDTO } from '@dtos/gestion-expediente/expediente-tratamiento-grid-dto';
import { ExpedienteTratamiento } from '@models/gestion-expediente/expediente-tratamiento';

@Injectable({
    providedIn: 'root'
})
export class ExpedienteTratamientoService {
    private dataUrl = 'expedienteTratamiento/';

    constructor(public http: HttpClient) {}

    consultar(idExpedienteTratamiento: number) {
        return this.http.get<ExpedienteTratamiento>(this.dataUrl + `consultar/${idExpedienteTratamiento}`);
    }

    consultarPorUsuario(idUsuario: number) {
        return this.http.get<ExpedienteTratamientoGridDTO[]>(this.dataUrl + `consultarPorUsuario/${idUsuario}`);
    }

    agregar() {
        return this.http.post<number>(this.dataUrl + 'agregar', {});
    }
    
}