import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Dominio } from '@models/catalogo/dominio';

@Injectable()
export class DominioService {
  private dataUrl = 'dominio/';

  constructor(public http: HttpClient) {}

  public consultar(idDominio: number): Observable<Dominio> {
    return this.http.get<Dominio>(this.dataUrl + `consultar/${idDominio}`);
  }

  public consultarPorNombre(nombre: string): Observable<Dominio> {
    return this.http.get<Dominio>(
      this.dataUrl + `consultarPorNombre/${nombre}`
    );
  }

  public consultarTodosParaGrid(): Observable<Dominio[]> {
    return this.http.get<Dominio[]>(this.dataUrl + 'consultarTodosParaGrid');
  }

  public agregar(dominio: Dominio): Observable<number> {
    return this.http.post<number>(this.dataUrl + 'agregar', dominio);
  }

  public editar(dominio: Dominio): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', dominio);
  }

  public eliminar(idDominio: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idDominio}`);
  }

  public consultarGeneral() {
    return this.http.get<Dominio[]>(this.dataUrl + 'consultarGeneral');
  }

  public consultarTodosParaSelector() {
    return this.http.get<Dominio[]>(
      this.dataUrl + 'consultarTodosParaSelector'
    );
  }
}
