import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Compania } from '@models/catalogo/compania';
import { Observable } from 'rxjs';

@Injectable()
export class CompaniaService {
  private dataUrl = 'compania/';

  constructor(public http: HttpClient) {}

  public consultar(idCompania: number) {
    return this.http.get<Compania>(this.dataUrl + `consultar/${idCompania}`);
  }

  public consultarGeneral() {
    return this.http.get<Compania[]>(this.dataUrl + 'consultarGeneral');
  }

  public consultarTodosParaSelector() {
    return this.http.get<Compania[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  public consultarPorUsuarioPermiso() {
    return this.http.get<Compania[]>(this.dataUrl + 'consultarPorUsuarioPermiso');
  }


  public consultarTodosParaGrid(filtro: any): Observable<Compania[]> {
    return this.http.post<Compania[]>(this.dataUrl + 'consultarTodosParaGrid', filtro);
  }

  public agregar(compania: Compania) {
    return this.http.post<number>(this.dataUrl + 'agregar', compania);
  }

  public editar(compania: Compania) {
    return this.http.put<void>(this.dataUrl + 'editar', compania);
  }

  public eliminar(idCompania: number) {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idCompania}`);
  }

  consultarPorUsuario() {
    return this.http.get<Compania>(this.dataUrl + `consultarPorUsuario`);
  }

  consultarClaveCompaniaUsuarioSesion() {
    return this.http.get(this.dataUrl + `consultarClaveCompaniaUsuarioSesion/`, {responseType: 'text'});
  }
}
