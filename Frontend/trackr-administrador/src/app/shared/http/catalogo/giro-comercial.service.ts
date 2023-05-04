import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { GiroComercial } from '@models/catalogo/giro-comercial';

@Injectable()
export class GiroComercialService {
  private dataUrl = 'giroComercial/';

  constructor(private http: HttpClient) { }

  public consultarTodos(): Observable<GiroComercial[]> {
    return this.http.get<GiroComercial[]>(this.dataUrl + 'consultarTodos');
  }

  public consultar(idGiroComercial: number): Observable<GiroComercial> {
    return this.http.get<GiroComercial>(this.dataUrl + `consultar/${idGiroComercial}`);
  }

  public agregar(tipoConcepto: GiroComercial): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'agregar', tipoConcepto);
  }

  public editar(tipoConcepto: GiroComercial): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', tipoConcepto);
  }

  public eliminar(idGiroComercial: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idGiroComercial}`);
  }
}
