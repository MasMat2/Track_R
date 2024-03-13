import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RolAcceso } from '@models/seguridad/rol-acceso';

@Injectable()
export class RolAccesoService {
  private dataUrl = 'rolAcceso/';

  constructor(public http: HttpClient) { }

  public consultarTodosParaSelector(): Observable<RolAcceso[]> {
    return this.http.get<RolAcceso[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

}
