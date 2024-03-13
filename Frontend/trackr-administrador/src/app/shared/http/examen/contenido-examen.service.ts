import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ContenidoExamen } from '@models/examen/contenido-examen';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ContenidoExamenService {
  private dataUrl = 'contenidoExamen/';

  constructor(public http: HttpClient) {}

  public consultarTodosParaSelector(): Observable<ContenidoExamen[]> {
    return this.http.get<ContenidoExamen[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  public consultar(idContenidoExamen: number): Observable<ContenidoExamen> {
    return this.http.get<ContenidoExamen>(this.dataUrl + `consultar/${idContenidoExamen}`);
  }

  public consultarGeneral(idTipoExamen: number): Observable<ContenidoExamen[]> {
    return this.http.get<ContenidoExamen[]>(this.dataUrl + `consultarGeneral/${idTipoExamen}`);
  }

  public agregar(contenidoExamen: ContenidoExamen): Observable<number> {
    return this.http.post<number>(this.dataUrl + 'agregar', contenidoExamen);
  }

  public editar(contenidoExamen: ContenidoExamen): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', contenidoExamen);
  }

  public eliminar(idContenidoExamen: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idContenidoExamen}`);
  }
}
