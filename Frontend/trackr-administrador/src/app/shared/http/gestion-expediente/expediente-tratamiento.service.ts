import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ExpedienteTratamientoGridDto } from '@dtos/gestion-expediente/expediente-tratamiento-grid-dto';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ExpedienteTratamientoService {
    private dataUrl = 'expedienteTratamiento/';

    constructor(private http: HttpClient) {}
    
    public consultarParaGrid(idUsuario: number): Observable<ExpedienteTratamientoGridDto[]> {
        return this.http.get<ExpedienteTratamientoGridDto[]>(this.dataUrl + `consultarParaGrid/${idUsuario}`);
    }
    
}