import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ArchivoCarga } from '@dtos/archivos/archivo-carga';
import { Blob } from 'buffer';
import { catchError, Observable, of } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ArchivoService {
    private dataUrl = 'archivo/';

    constructor(public http: HttpClient) {}

    public obtenerUsuarioImagen(idUsuario: number): Observable<any> {
        return this.http.get(this.dataUrl + `usuario/${idUsuario}`, {
            responseType: 'blob'
        }).pipe(
            catchError(() => of(null) )
        );
    }

    public obtenerUsuarioEnSesionImagen(): Observable<any> {
        return this.http.get(this.dataUrl + 'usuarioEnSesionImagen', {
            responseType: 'blob'
        });
    }

    public descargarPlantillaCargaMasivaEstados(): Observable<any> {
        return this.http.get(this.dataUrl + 'descargarPlantillaCargaMasivaEstados', {
            responseType: 'blob'
        });
    }

    public subirArchivoCargaMasivaEstados(archivo: ArchivoCarga): Observable<any> {
      

        return this.http.post(this.dataUrl + 'subirArchivoCargaMasivaEstados',archivo);
    }

    public descargarPlantillaCargaMasivaMunicipios(): Observable<any>{
        return this.http.get(this.dataUrl + 'descargarPlantillaCargaMasivaMunicipios', {
            responseType: 'blob'
        });
    }

    public descargarPlantillaCargaMasivaCodigosPostales(): Observable<any> {
        return this.http.get(this.dataUrl + 'descargarPlantillaCargaMasivaCodigosPostales', {
            responseType: 'blob'
        });
    }
}