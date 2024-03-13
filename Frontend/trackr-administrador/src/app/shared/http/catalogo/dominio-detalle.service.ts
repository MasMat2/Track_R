import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DominioDetalle } from '@models/catalogo/dominio-detalle';

@Injectable()
export class DominioDetalleService {
  private dataUrl = 'dominioDetalle/';

  constructor(public http: HttpClient) { }

  public consultar(idDominioDetalle: number): Observable<DominioDetalle> {
    return this.http.get<DominioDetalle>(this.dataUrl + `consultar/${idDominioDetalle}`);
  }

  public consultarPorDominio(idDominio: number): Observable<DominioDetalle[]> {
    return this.http.get<DominioDetalle[]>(this.dataUrl + `consultarPorDominio/${idDominio}`);
  }

  public agregar(dominioDetalle: DominioDetalle): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'agregar', dominioDetalle);
  }

  public editar(dominioDetale: DominioDetalle): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', dominioDetale);
  }

  public eliminar(idDominioDetalle: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idDominioDetalle}`);
  }

}
