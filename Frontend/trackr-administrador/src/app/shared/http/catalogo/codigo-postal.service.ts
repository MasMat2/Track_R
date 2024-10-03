import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CodigoPostal } from '@models/catalogo/codigo-postal';


@Injectable()
export class CodigoPostalService {
  private dataUrl = 'codigoPostal/';

  constructor(public http: HttpClient) {}

  consultar(idCodigoPostal: number): Observable<CodigoPostal> {
    return this.http.get<CodigoPostal>(this.dataUrl + `consultar/${idCodigoPostal}`);
  }

  consultarTodosParaGrid(): Observable<CodigoPostal[]> {
    return this.http.get<CodigoPostal[]>(this.dataUrl + 'consultarTodosParaGrid');
  }

  consultarTodosParaSelector(): Observable<CodigoPostal[]> {
    return this.http.get<CodigoPostal[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  consultarPorCodigoPostal(codigoPostal: string): Observable<CodigoPostal[]> {
    return this.http.get<CodigoPostal[]>(this.dataUrl + `consultarPorCodigoPostal/${codigoPostal}`);
  }

  consultarPorMunicipio(idMunicipio: number): Observable<CodigoPostal[]> {
    return this.http.get<CodigoPostal[]>(this.dataUrl + `consultarPorMunicipio/${idMunicipio}`);
  }

  consultarPorPaisBusqueda(codigoPostal: string, idPais: number): Observable<CodigoPostal[]> {
    return this.http.get<CodigoPostal[]>(this.dataUrl + `consultarPorPaisBusqueda/${codigoPostal}/${idPais}`);
  }

  agregar(codigoPostal: CodigoPostal): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'agregar', codigoPostal);
  }

  editar(codigoPostal: CodigoPostal): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', codigoPostal);
  }

  eliminar(idCodigoPostal: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idCodigoPostal}`);
  }

  sincronizarEstadosExcel(): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'actualizarCodigosPostalesExcel', null);
  }
}
