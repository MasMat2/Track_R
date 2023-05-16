import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Seccion } from '@models/gestion-entidad/seccion';
import { Observable } from 'rxjs';

@Injectable()
export class SeccionService {
  private dataUrl = 'seccion/';

  constructor(public http: HttpClient) {}

  public consultarGeneral(): Observable<Seccion[]> {
    return this.http.get<Seccion[]>(this.dataUrl + 'consultarGeneral');
  }

  public consultar(idSeccion: number): Observable<Seccion> {
    return this.http.get<Seccion>(this.dataUrl + `consultar/${idSeccion}`);
  }

  public agregar(seccion: Seccion): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'agregar', seccion);
  }

  public editar(seccion: Seccion): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', seccion);
  }

  public eliminar(idSeccion: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idSeccion}`);
  }
}
