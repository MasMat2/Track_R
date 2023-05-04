import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Estado } from '@models/catalogo/estado';

@Injectable()
export class EstadoService {
  private dataUrl = 'estado/';

  constructor(public http: HttpClient) {}

  consultar(idEstado: number): Observable<Estado> {
    return this.http.get<Estado>(this.dataUrl + `consultar/${idEstado}`);
  }

  consultarGeneral() {
    return this.http.get<Estado[]>(this.dataUrl + 'consultarGeneral');
  }

  consultarTodosParaGrid(): Observable<Estado[]> {
    return this.http.get<Estado[]>(this.dataUrl + 'consultarTodosParaGrid');
  }

  consultarPorPaisParaSelector(idPais: number): Observable<Estado[]> {
    return this.http.get<Estado[]>(this.dataUrl + `consultarPorPaisParaSelector/${idPais}`);
  }

  agregar(estado: Estado): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'agregar', estado);
  }

  editar(estado: Estado): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', estado);
  }

  eliminar(idEstado: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idEstado}`);
  }
}
