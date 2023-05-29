import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ExpedienteWrapper } from '@dtos/seguridad/expediente-wrapper';

@Injectable({
    providedIn: 'root'
})
export class ExpedienteTrackrService {
    private dataUrl = 'expedientetrackr/';

    constructor(public http: HttpClient) {}

    // TODO: Change ExpedienteWrapper location
    public consultarWrapperPorUsuario(idUsuario: number) {
        return this.http.get<ExpedienteWrapper>(this.dataUrl + `consultarWrapperPorUsuario/${idUsuario}`);
    }

    public agregarWrapper(expedienteWrapper: ExpedienteWrapper) {
        return this.http.post<any>(this.dataUrl + 'agregarWrapper', expedienteWrapper);
    }

    public editarWrapper(expedienteWrapper: ExpedienteWrapper) {
        return this.http.post<any>(this.dataUrl + 'editarWrapper', expedienteWrapper);
    }


}