import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { MetodoPago } from '@models/gestion-caja/metodo-pago';

@Injectable()
export class MetodoPagoService {
  private dataUrl = 'metodoPago/';

  constructor(public http: HttpClient) {}

  consultarTodos(): Observable<MetodoPago[]> {
    return this.http.get<MetodoPago[]>(this.dataUrl + `consultarTodos`);
  }

  consultarPorClave(clave: string): Observable<MetodoPago> {
    return this.http.get<MetodoPago>(this.dataUrl + `consultarPorClave/${clave}`);
  }
}
