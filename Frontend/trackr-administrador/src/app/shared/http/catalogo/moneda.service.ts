import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Moneda } from '@models/catalogo/moneda';

@Injectable()
export class MonedaService {
  private dataUrl = 'moneda/';

  constructor(public http: HttpClient) { }

  consultarPorId(idMoneda: number): Observable<Moneda> {
    return this.http.get<Moneda>(this.dataUrl + `consultarPorId/${idMoneda}`);
  }

  consultarTodosParaGrid(): Observable<Moneda[]> {
    return this.http.get<Moneda[]>(this.dataUrl + 'consultarTodosParaGrid');
  }

  consultarParaSelector(): Observable<Moneda[]> {
    return this.http.get<Moneda[]>(this.dataUrl + 'consultarParaSelector');
  }

  agregar(moneda: Moneda){
    return this.http.post<number>(this.dataUrl + 'agregar', moneda);
  }

  editar(moneda: Moneda): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', moneda);
  }

  eliminar(idMoneda: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idMoneda}`);
  }
}
