import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Asignatura } from '@models/examen/asignatura';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AsignaturaService {
  private dataUrl = 'asignatura/';

  constructor(private http: HttpClient) {}

  public consultarTodosParaSelector(): Observable<Asignatura[]> {
    return this.http.get<Asignatura[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  public consultar(idAsignatura: number): Observable<Asignatura> {
    return this.http.get<Asignatura>(this.dataUrl + `consultar/${idAsignatura}`);
  }

  public consultarGeneral(): Observable<Asignatura[]> {
    return this.http.get<Asignatura[]>(this.dataUrl + 'consultarGeneral');
  }

  public agregar(asignatura: Asignatura): Observable<number> {
    return this.http.post<number>(this.dataUrl + 'agregar', asignatura);
  }

  public editar(asignatura: Asignatura): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', asignatura);
  }

  public eliminar(idAsignatura: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idAsignatura}`);
  }
}
