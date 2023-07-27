import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Acceso } from 'src/app/models/acceso';

@Injectable({
  providedIn: 'root'
})
export class AccesoService {
  private dataUrl = 'acceso/';

  constructor(public http: HttpClient) {}

  public consultar(idAcceso: number): Observable<Acceso> {
    return this.http.get<Acceso>(this.dataUrl + `consultar/${idAcceso}`);
  }

}
