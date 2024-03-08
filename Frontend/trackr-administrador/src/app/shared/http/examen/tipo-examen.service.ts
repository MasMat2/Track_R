import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TipoExamen } from '@models/examen/tipo-examen';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TipoExamenService {
  private dataUrl = 'TipoExamen/';

  constructor(public http: HttpClient) {}

  public consultarTodosParaSelector(): Observable<TipoExamen[]> {
    return this.http.get<TipoExamen[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  public consultar(idTipoExamen: number): Observable<TipoExamen> {
    return this.http.get<TipoExamen>(this.dataUrl + `consultar/${idTipoExamen}`);
  }

  public consultarGeneral(): Observable<TipoExamen[]> {
    return this.http.get<TipoExamen[]>(this.dataUrl + 'consultarGeneral');
  }

  public agregar(tipoExamen: TipoExamen): Observable<number> {
    return this.http.post<number>(this.dataUrl + 'agregar', tipoExamen);
  }

  public editar(tipoExamen: TipoExamen): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', tipoExamen);
  }

  public eliminar(idTipoExamen: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idTipoExamen}`);
  }
}
