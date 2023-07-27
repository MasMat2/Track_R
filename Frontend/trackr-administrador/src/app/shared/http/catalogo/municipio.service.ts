import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Municipio } from '@models/catalogo/municipio';

@Injectable()
export class MunicipioService {
    private dataUrl = 'municipio/';

    constructor(public http: HttpClient) {}

    consultar(idMunicipio: number): Observable<Municipio> {
       return this.http.get<Municipio>(this.dataUrl + `consultar/${idMunicipio}`);
    }

    consultarTodosParaGrid(): Observable<Municipio[]> {
    return this.http.get<Municipio[]>(this.dataUrl + 'consultarTodosParaGrid');
    }

    consultarPorEstadoParaSelector(idEstado: number): Observable<Municipio[]> {
    return this.http.get<Municipio[]>(this.dataUrl + `consultarPorEstadoParaSelector/${idEstado}`);
    }

    agregar(municipio: Municipio): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'agregar', municipio);
    }

    editar(municipio: Municipio): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', municipio);
    }

    eliminar(idEstado: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idEstado}`);
    }

    public consultarTodosParaSelector() {
      return this.http.get<Municipio[]>(this.dataUrl + 'consultarTodosParaSelector');
    }
}
