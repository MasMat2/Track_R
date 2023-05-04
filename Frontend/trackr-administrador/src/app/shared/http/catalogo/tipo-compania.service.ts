import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TipoCompania } from '@models/catalogo/tipo-compania';

@Injectable()
export class TipoCompaniaService {
  private dataUrl = 'tipoCompania/';

  constructor(public http: HttpClient) {}

  public consultarParaSelector(): Observable<TipoCompania[]> {
    return this.http.get<TipoCompania[]>(this.dataUrl + 'consultarParaSelector');
  }

  public consultar(idTipoCompania: number): Observable<TipoCompania> {
    return this.http.get<TipoCompania>(this.dataUrl + `consultar/${idTipoCompania}`);
  }

  public consultarTodosParaGrid(): Observable<TipoCompania[]> {
    return this.http.get<TipoCompania[]>(this.dataUrl + `consultarTodosParaGrid`);
  }

  public consultarPorClave(claveTipoCompania: string): Observable<TipoCompania> {
    return this.http.get<TipoCompania>(this.dataUrl + `consultarPorClave/${claveTipoCompania}`);
  }

  public agregar(tipoCompania: TipoCompania): Observable<void> {
    return this.http.post<void>(this.dataUrl + `agregar`, tipoCompania);
  }

  public editar(tipoCompania: TipoCompania): Observable<void> {
    return this.http.put<void>(this.dataUrl + `editar`, tipoCompania);
  }
  public eliminar(idTipoCompania: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idTipoCompania}`);
  }

}
