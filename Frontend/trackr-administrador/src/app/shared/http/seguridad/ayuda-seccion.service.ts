import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AyudaSeccion } from '@models/seguridad/ayuda-seccion';

@Injectable({
  providedIn: 'root'
})
export class AyudaSeccionService {
  private dataUrl = 'ayudaSeccion/';

  constructor(public http: HttpClient) {}

  public consultarTodosParaSelector() {
    return this.http.get<AyudaSeccion[]>(this.dataUrl + `consultarTodosParaSelector`);
  }

}
