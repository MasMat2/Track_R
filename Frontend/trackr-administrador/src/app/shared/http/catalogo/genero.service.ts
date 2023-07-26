import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
<<<<<<< HEAD
import { GeneroDto} from '@dtos/catalogos/GeneroDto';
=======
import { GeneroDto } from '@dtos/catalogo/generoDto';
>>>>>>> a871e4299abd973a0949004ca3fe85330f7e4b43


@Injectable()
export class GeneroService {
    private dataUrl = "genero/";  // URL to web api

    constructor(public http: HttpClient) { }

    consultar(idGenero: number): Observable<GeneroDto> {
        return this.http.get<GeneroDto>(this.dataUrl + `${idGenero}`);
    }

    consulta(): Observable<GeneroDto[]> {
        return this.http.get<GeneroDto[]>(this.dataUrl);
    }
    agregar(genero: GeneroDto): Observable<void> {
        return this.http.post<void>(this.dataUrl,genero);
    }

    editar(genero: GeneroDto): Observable<void> {
        return this.http.put<void>(this.dataUrl, genero);
    }

    eliminar(idGenero: number): Observable<void> {
        return this.http.delete<void>(this.dataUrl + `${idGenero}`);
    }

}
