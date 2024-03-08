import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ListaPrecio } from '@models/catalogo/lista-precio';

@Injectable()
export class ListaPrecioService {
  private dataUrl = 'listaPrecio/';

  constructor(public http: HttpClient) { }

  public consultarTodosParaSelector(): Observable<ListaPrecio[]> {
    return this.http.get<ListaPrecio[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  public consultarTodosParaGrid(): Observable<ListaPrecio[]> {
    return this.http.get<ListaPrecio[]>(this.dataUrl + 'consultarTodosParaGrid');
  }

  public consultarTodosPorHospitalParaSelector(): Observable<ListaPrecio[]> {
    return this.http.get<ListaPrecio[]>(this.dataUrl + 'consultarTodosPorHospitalParaSelector');
  }

  public consultar(idListaPrecio: number): Observable<ListaPrecio> {
    return this.http.get<ListaPrecio>(this.dataUrl + `consultar/${idListaPrecio}`);
  }

  public consultarVigente(): Observable<ListaPrecio[]> {
    return this.http.get<ListaPrecio[]>(this.dataUrl + `consultarVigente`);
  }

  public agregar(listaPrecio: ListaPrecio): Observable<number> {
    return this.http.post<number>(this.dataUrl + 'agregar', listaPrecio);
  }

  public editar(listaPrecio: ListaPrecio): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', listaPrecio);
  }

  public eliminar(idListaPrecio: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idListaPrecio}`);
  }

  public consultarPorPresentacion(idPresentacion: number): Observable<ListaPrecio> {
    return this.http.get<ListaPrecio>(this.dataUrl + `consultarPorPresentacion/${idPresentacion}`);
  }

  public copiar(listaPrecio: any): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'copiar', listaPrecio);
  }
}
