import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ExpedienteTratamientoGridDto } from '@dtos/gestion-expediente/expediente-tratamiento-grid-dto';
import { ExpedienteTratamiento } from '@models/gestion-expediente/expediente-tratamiento';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ExpedienteTratamientoService {
    private dataUrl = 'expedienteTratamiento/';

    constructor(public http: HttpClient) {}

    public consultar(idExpedienteTratamiento: number): Observable<ExpedienteTratamiento> {
        return this.http.get<ExpedienteTratamiento>(this.dataUrl + `${idExpedienteTratamiento}`);
    }

    public consultarPorUsuario(idUsuario: number): Observable<ExpedienteTratamientoGridDto[]> {
        return this.http.get<ExpedienteTratamientoGridDto[]>(this.dataUrl + `usuario/${idUsuario}`);
    }
    
}