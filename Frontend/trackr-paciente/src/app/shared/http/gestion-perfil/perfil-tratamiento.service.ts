import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable, map } from 'rxjs';
import { ExpedienteTratamientoDetalleDto } from '../../Dtos/gestion-perfil/expediente-tratamiento-detalle-dto';
import { ExpedienteTratamientoPerfilDto } from '../../Dtos/gestion-perfil/expediente-tratamiento-perfil-dto';

@Injectable({
    providedIn: 'root'
})
export class PerfilTratamientoService {
    private dataUrl = 'expedienteTratamiento/';

    constructor(public http: HttpClient) { }

    public consultarTratamientos(): Observable<ExpedienteTratamientoPerfilDto[]> {
        return this.http.get<ExpedienteTratamientoPerfilDto[]>(this.dataUrl + `consultarTratamientosTrackr`);
    }
    public consultarTratamientoDetalle(idExpedienteTratamiento: number): Observable<ExpedienteTratamientoDetalleDto> {
        return this.http.get<ExpedienteTratamientoDetalleDto>(this.dataUrl + `consultarTratamientoDetalle/${idExpedienteTratamiento}`);
    }

    public agregar(perfilTratamientoDto: ExpedienteTratamientoDetalleDto) {
        return this.http.post<number>(this.dataUrl + 'agregar', perfilTratamientoDto);
    }

    public editarTratamiento(perfilTratamientoDto: ExpedienteTratamientoDetalleDto) {
        return this.http.put<number>(this.dataUrl + 'editar', perfilTratamientoDto);
    }

    public eliminarTratamiento(idExpedienteTratamiento: number): Observable<void> {
        return this.http.delete<void>(this.dataUrl + `eliminar/${idExpedienteTratamiento}`);
    }

    // public selectorDeDoctor(): Observable<SelectorDto[]> {
    //     return this.http.get<SelectorDto[]>(this.dataUrl + `selectorDeDoctor`);
    // }

    // public selectorPadecimeintos(): Observable<SelectorDto[]> {
    //     return this.http.get<SelectorDto[]>(this.dataUrl + `selectorDePadecimiento`);
    // }

}