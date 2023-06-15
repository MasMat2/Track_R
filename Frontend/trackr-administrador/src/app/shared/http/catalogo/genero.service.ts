import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Genero } from "@models/catalogo/genero";


@Injectable()
export class GeneroService {
    private dataUrl = "genero/";  // URL to web api

    constructor(public http: HttpClient) { }

    consultar(idGenero: number): Observable<GeneroDto> {
        return this.http.get<GeneroDto>(this.dataUrl + `consultar/${idGenero}`);
    }

    consulta(): Observable<GeneroDto[]> {
        return this.http.get<GeneroDto[]>(this.dataUrl + 'consultar');
    }
    agregar(generoDto: Genero): Observable<void> {
        return this.http.post<void>(this.dataUrl + 'agregar', Genero);
    }

    editar(generoDto: Genero): Observable<void> {
        return this.http.put<void>(this.dataUrl + 'editar', Genero);
    }

    eliminar(idGenero: number): Observable<void> {
        return this.http.delete<void>(this.dataUrl + `eliminar/${idGenero}`);
    }

}
