import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CodigoPostal } from '@models/catalogo/codigo-postal';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CodigoPostalService {

  private dataUrl = 'codigoPostal/';

  constructor(public http: HttpClient) { }


  consultarPorCodigoPostal(codigoPostal: string): Observable<CodigoPostal[]> {
    return this.http.get<CodigoPostal[]>(this.dataUrl + `consultarPorCodigoPostal/${codigoPostal}`);
  }
}
