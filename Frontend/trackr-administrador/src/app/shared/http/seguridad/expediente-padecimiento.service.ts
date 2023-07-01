import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ExpedientePadecimientoDTO } from '@dtos/seguridad/expediente-padecimiento-dto';
import { ExpedientePadecimientoGridDTO } from '@dtos/seguridad/expediente-padecimiento-grid-dto';
import { ExpedientePadecimientoSelectorDTO } from '@dtos/seguridad/expediente-padecimiento-selector-dto';

@Injectable({
    providedIn: 'root'
})
export class ExpedientePadecimientoService {
    private dataUrl = 'expedientePadecimiento/';

    constructor(public http: HttpClient) {}

    public consultarPorUsuario(idUsuario: number) {
        return this.http.get<ExpedientePadecimientoDTO>(this.dataUrl + `consultarPorUsuario/${idUsuario}`);
    }

    public consultarParaGridPorUsuario(idUsuario: number) {
        return this.http.get<ExpedientePadecimientoGridDTO[]>(this.dataUrl + `gridPorUsuario/${idUsuario}`);
    }

    public consultarParaSelector(){
        return this.http.get<ExpedientePadecimientoSelectorDTO[]>(this.dataUrl + `consultarParaSelector`);
    }

}