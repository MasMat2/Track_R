import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { ExpedienteWrapper } from '../../Dtos/seguridad/expediente-wrapper';
import { UsuarioExpedienteSidebarDTO } from '../../../../../../trackr-administrador/src/app/shared/dtos/seguridad/usuario-expediente-sidebar-dto';
import { ApegoTomaMedicamentoDto } from '../../../../../../trackr-administrador/src/app/shared/dtos/gestion-expediente/apego-toma-medicamento-dto';
import { UsuarioExpedienteGridDTO } from '@dtos/seguridad/usuario-expediente-grid-dto';

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
    public consultaParaSidebar(idUsuario:number){
        return this.http.get<UsuarioExpedienteSidebarDTO>(this.dataUrl + `sidebar/${idUsuario}`);
    }


    public eliminar(idExpediente: number): Observable<void> {
        return this.http.delete<void>(this.dataUrl + `eliminar/${idExpediente}`);
    }
    public apegoTratamientos() :  Observable<ApegoTomaMedicamentoDto[]> {
        return this.http.get<ApegoTomaMedicamentoDto[]>(this.dataUrl + 'apegoMedicamentoUsuarios');
    }

}
