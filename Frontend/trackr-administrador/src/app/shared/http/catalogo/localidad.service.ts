import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Localidad } from '@models/catalogo/localidad';

@Injectable()
export class LocalidadService{
    private dataUrl = 'localidad/';

    constructor(public http: HttpClient) {}

    consultarPorEstado(idEstado: number): Observable<Localidad[]> {
      return this.http.get<Localidad[]>(this.dataUrl + `consultarPorEstado/${idEstado}`);
    }
}
