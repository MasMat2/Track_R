import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Respuesta } from '@models/examen/respuesta';

@Injectable({
    providedIn: 'root'
})
export class RespuestaService {
    private dataUrl = 'respuesta/';

    constructor(private http: HttpClient) { }

    public consultarTodosPorReactivo(idReactivo:number): Observable<Respuesta[]> {
        return this.http.get<Respuesta[]>(this.dataUrl + `consultarTodosPorReactivo/${idReactivo}`);
    }

    public consultarParaFormulario(idRespuesta: number): Observable<Respuesta> {
        return this.http.get<Respuesta>(this.dataUrl + `consultar/${idRespuesta}`);
    }

    public agregar(respuesta: Respuesta): Observable<number> {
        return this.http.post<number>(this.dataUrl + 'agregar', respuesta);
    }

    public editar(respuesta: Respuesta): Observable<void> {
        return this.http.put<void>(this.dataUrl + 'editar', respuesta);   
    }

    public eliminar(idRespuesta: number): Observable<void> {
        return this.http.delete<void>(this.dataUrl + `eliminar/${idRespuesta}`);
    }

}