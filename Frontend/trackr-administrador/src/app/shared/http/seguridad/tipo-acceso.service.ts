import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { TipoAcceso } from '@models/seguridad/tipo-acceso';

@Injectable()
export class TipoAccesoService {
  private dataUrl = 'tipoAcceso/';

  constructor(public http: HttpClient) {}

  public consultarGeneral(): Observable<TipoAcceso[]> {
    return this.http.get<TipoAcceso[]>(this.dataUrl + 'consultarGeneral');
  }
}
