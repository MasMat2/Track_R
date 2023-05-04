import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Colonia } from '@models/catalogo/colonia';

@Injectable()
export class ColoniaService {
  private dataUrl = 'colonia/';

  constructor(public http: HttpClient) {}

  consultarParaGrid(): Observable<Colonia[]>{
    return this.http.get<Colonia[]>(this.dataUrl + "consultarParaGrid");
  }

  consultarPorCodigoParaSelector(codigoPostal: string): Observable<Colonia[]> {
    return this.http.get<Colonia[]>(this.dataUrl + `consultarPorCodigoParaSelector/${codigoPostal}`);
  }

  agregar(colonia: Colonia): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'agregar', colonia);
  }

  editar(colonia: Colonia): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', colonia);
  }

  eliminar(idColonia: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idColonia}`);
  }
}
