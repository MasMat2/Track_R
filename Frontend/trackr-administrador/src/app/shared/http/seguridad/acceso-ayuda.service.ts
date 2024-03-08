import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AccesoAyuda } from '@models/seguridad/acceso-ayuda';
import { AccesoAyudaSeccionado } from '@models/seguridad/acceso-ayuda-seccionado';

@Injectable(
  {
    providedIn: 'root'
  }
)
export class AccesoAyudaService {
  private dataUrl = 'accesoAyuda/';

  constructor(public http: HttpClient) {}

  public consultar(idAccesoAyuda: number): Observable<AccesoAyuda> {
    return this.http.get<AccesoAyuda>(this.dataUrl + `consultar/${idAccesoAyuda}`);
  }
  public consultarPorAcceso(idAcceso: number) {
    return this.http.get<AccesoAyuda[]>(this.dataUrl + `consultarPorAcceso/${idAcceso}`);
  }
  public consultarPorAccesoPorSeccion(idAcceso: number) {
    return this.http.get<AccesoAyudaSeccionado[]>(this.dataUrl + `consultarPorAccesoPorSeccion/${idAcceso}`);
  }

  public agregar(accesoAyuda: AccesoAyuda): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'agregar', accesoAyuda);
  }

  public editar(accesoAyuda: AccesoAyuda): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', accesoAyuda);
  }

  public eliminar(idAccesoAyuda: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idAccesoAyuda}`);
  }

}
