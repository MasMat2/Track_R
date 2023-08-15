import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PerfilTratamientoDto } from '@dtos/gestion-perfil/perfil-tratamiento-dto';
import { SelectorDto } from '@dtos/gestion-perfil/selector-dto';

import { Observable, map } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class PerfilTratamientoService {
    private dataUrl = 'expedienteTratamiento/';

    constructor(public http: HttpClient) { }

    agregar(perfilTratamientoDto: PerfilTratamientoDto) {
        return this.http.post<number>(this.dataUrl + 'agregar', perfilTratamientoDto);
    }

    consultarTratamientos(idUsuario: number): Observable<PerfilTratamientoDto[]> {
        return this.http.get<PerfilTratamientoDto[]>(this.dataUrl + `usuario/${idUsuario}`);
    }

    selectorDeDoctor(): Observable<SelectorDto[]> {
        return this.http.get<SelectorDto[]>(this.dataUrl + `selectorDeDoctor`);
    }

    selectorPadecimeintos(): Observable<SelectorDto[]> {

        return this.http.get<SelectorDto[]>(this.dataUrl + `selectorDePadecimiento/5315`);
    }

}