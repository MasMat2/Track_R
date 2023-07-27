import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Estado } from '@models/catalogo/estado';
import { TituloAcademico } from '@models/catalogo/titulo-academico';

@Injectable()
export class TituloAcademicoService {
  private dataUrl = 'tituloAcademico/';

  constructor(public http: HttpClient) {}

  public consultarTodosParaSelector(): Observable<TituloAcademico[]> {
    return this.http.get<TituloAcademico[]>(this.dataUrl + `consultarTodosParaSelector`);
  }

  public consultarTodosParaGrid() {
    return this.http.get<TituloAcademico[]>(this.dataUrl + 'consultarTodosParaGrid');
  }

  public consultar(idTituloAcademico: number) {
    return this.http.get<TituloAcademico>(this.dataUrl + `consultar/${idTituloAcademico}`);
  }

  public agregar(tituloAcademico: TituloAcademico) {
    return this.http.post<void>(this.dataUrl + 'agregar', tituloAcademico);
  }

  public editar(tituloAcademico: TituloAcademico) {
    return this.http.put<void>(this.dataUrl + 'editar', tituloAcademico);
  }

  public eliminar(idTituloAcademico: number) {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idTituloAcademico}`);
  }
}
