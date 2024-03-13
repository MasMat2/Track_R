import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Area } from '@models/catalogo/area';

@Injectable({
  providedIn: 'root'
})
export class AreaService {
  private dataUrl = 'area/';

  constructor(public http: HttpClient) {}

  public consultarTodosParaGrid(): Observable<Area[]> {
    return this.http.get<Area[]>(this.dataUrl + 'consultarParaSelector');
  }

  public consultarTodosParaSelector(): Observable<Area[]> {
    return this.http.get<Area[]>(this.dataUrl + 'consultarParaSelector');
  }

  public consultar(idArea: number): Observable<Area> {
    return this.http.get<Area>(this.dataUrl + `consultar/${idArea}`);
  }

  public agregar(area: Area): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'agregar', area);
  }

  public editar(area: Area): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', area);
  }

  public eliminar(idArea: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idArea}`);
  }
}
