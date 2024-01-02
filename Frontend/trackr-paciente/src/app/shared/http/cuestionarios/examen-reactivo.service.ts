import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ExamenReactivo } from '@models/examen/examen-reactivo';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExamenReactivoService {
  private dataUrl = 'examenReactivo/';

  constructor(public http: HttpClient) {}

  public consultarReactivosExamen(idExamen: number): Observable<ExamenReactivo[]> {
    return this.http.get<ExamenReactivo[]>(this.dataUrl + `consultarReactivosExamen/${idExamen}`);
  }

  public revisar(examenReactivoList: ExamenReactivo[]): Observable<number> {
    return this.http.post<number>(this.dataUrl + 'revisar', examenReactivoList);
  }
}
