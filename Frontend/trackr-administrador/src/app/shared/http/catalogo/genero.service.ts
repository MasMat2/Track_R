import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Genero } from "@models/catalogo/genero";


@Injectable()
export class GeneroService {
    private dataUrl = "genero/";  // URL to web api

    constructor(public http: HttpClient) { }
    
    consultar(idUsuario: number): Observable<Genero> {
        return this.http.get<Genero>(this.dataUrl + `consultar/${idUsuario}`);
    }


    }
    