import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NivelExamen } from '@models/examen/nivel-examen';
import { Observable } from 'rxjs';

@Injectable()
export class NivelExamenService {
  private dataUrl = 'nivelExamen/';

  constructor(public http: HttpClient) {}

  public consultarTodosParaSelector(): Observable<NivelExamen[]> {
    return this.http.get<NivelExamen[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  public consultar(idNivelExamen: number): Observable<NivelExamen> {
    return this.http.get<NivelExamen>(this.dataUrl + `consultar/${idNivelExamen}`);
  }

  public consultarGeneral(): Observable<NivelExamen[]> {
    return this.http.get<NivelExamen[]>(this.dataUrl + 'consultarGeneral');
  }

  public agregar(nivelExamen: NivelExamen): Observable<number> {
    return this.http.post<number>(this.dataUrl + 'agregar', nivelExamen);
  }

  public editar(nivelExamen: NivelExamen): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', nivelExamen);
  }

  public eliminar(idNivelExamen: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idNivelExamen}`);
  }
}
