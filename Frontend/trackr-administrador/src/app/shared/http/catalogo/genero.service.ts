import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GeneroDto} from '@dtos/catalogos/GeneroDto';


@Injectable()
export class GeneroService {
    private dataUrl = "genero/";  // URL to web api

    constructor(public http: HttpClient) { }

    consultar(idGenero: number): Observable<GeneroDto> {
        return this.http.get<GeneroDto>(this.dataUrl + `consultar/${idGenero}`);
    }

    consulta(): Observable<GeneroDto[]> {
        return this.http.get<GeneroDto[]>(this.dataUrl + 'consulta');
    }
    agregar(genero: GeneroDto): Observable<void> {
        return this.http.post<void>(this.dataUrl + 'agregar', genero);
    }

    editar(genero: GeneroDto): Observable<void> {
        return this.http.put<void>(this.dataUrl + 'editar', genero);
    }

    eliminar(idGenero: number): Observable<void> {
        return this.http.delete<void>(this.dataUrl + `eliminar/${idGenero}`);
    }

}
