import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ExpedienteWrapper } from '@dtos/seguridad/expediente-wrapper';
import { UsuarioExpedienteGridDTO } from '@dtos/seguridad/usuario-expediente-grid-dto';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ExpedienteTrackrService {
    private dataUrl = 'expedientetrackr/';

    constructor(public http: HttpClient) {}

    public consultarWrapperPorUsuario(idUsuario: number) {
        return this.http.get<ExpedienteWrapper>(this.dataUrl + `consultarWrapperPorUsuario/${idUsuario}`);
    }

    public agregarWrapper(expedienteWrapper: ExpedienteWrapper) {
        return this.http.post<any>(this.dataUrl + 'agregarWrapper', expedienteWrapper);
    }

    public editarWrapper(expedienteWrapper: ExpedienteWrapper) {
        return this.http.post<any>(this.dataUrl + 'editarWrapper', expedienteWrapper);
    }

    public consultarParaGrid() {
        return this.http.get<UsuarioExpedienteGridDTO[]>(this.dataUrl + `consultarParaGrid/`);
    }

    public eliminar(idExpediente: number): Observable<void> {
        return this.http.delete<void>(this.dataUrl + `eliminar/${idExpediente}`);
    }

}
