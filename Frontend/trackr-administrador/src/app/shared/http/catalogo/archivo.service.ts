import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Blob } from 'buffer';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ArchivoService {
    private dataUrl = 'archivo/';

    constructor(public http: HttpClient) {}

    public obtenerUsuarioImagen(idUsuario: number): Observable<any> {
        return this.http.get(this.dataUrl + `usuario/${idUsuario}`, {
            responseType: 'blob'
        });
    }

    public obtenerUsuarioEnSesionImagen(): Observable<any> {
        return this.http.get(this.dataUrl + 'usuarioEnSesionImagen', {
            responseType: 'blob'
        });
    }
}