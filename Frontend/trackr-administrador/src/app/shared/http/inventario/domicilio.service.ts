import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Domicilio } from '@models/seguridad/domicilio';

@Injectable({
    providedIn: 'root'
})
export class DomicilioService {
    private dataUrl = 'domicilio/';

    constructor(public http: HttpClient) {}

    // TODO: Change ExpedienteWrapper location
    public consultar(idDomicilio: number) {
        return this.http.get<Domicilio>(this.dataUrl + `consultar/${idDomicilio}`);
    }
}