import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Acceso } from '@models/seguridad/acceso';
import { AccesoMenuDto } from '@dtos/seguridad/acceso-menu-dto';

@Injectable({
  providedIn: 'root'
})
export class AccesoService {
  private dataUrl = 'acceso/';

  constructor(public http: HttpClient) {}

  public consultarGeneral(): Observable<Acceso[]> {
    return this.http.get<Acceso[]>(this.dataUrl + `consultarGeneral`);
  }

  public consultar(idAcceso: number): Observable<Acceso> {
    return this.http.get<Acceso>(this.dataUrl + `consultar/${idAcceso}`);
  }
  public consultarPorClave(claveAcceso: string): Observable<Acceso> {
    return this.http.get<Acceso>(this.dataUrl + `consultarPorClave/${claveAcceso}`);
  }

  public tieneAcceso(codigoAcceso: string): Observable<boolean> {
    return this.http.get<boolean>(this.dataUrl + `tieneAcceso/${codigoAcceso}`);
  }

  public tieneAccesoLista(listaAccesos: string[]): Observable<Acceso[]> {
    return this.http.post<Acceso[]>(this.dataUrl + `tieneAccesoLista/`, listaAccesos);
  }

  public consultarParaReporteArbol(idRolAcceso: number): Observable<Acceso[]> {
    return this.http.get<Acceso[]>(this.dataUrl + `consultarParaReporteArbol/${idRolAcceso}`);
  }

  public consultarMenu(): Observable<AccesoMenuDto[]> {
    return this.http.get<Acceso[]>(this.dataUrl + `consultarMenu`);
  }

  public agregar(acceso: Acceso): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'agregar', acceso);
  }

  public editar(acceso: Acceso): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', acceso);
  }

  public eliminar(idAcceso: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idAcceso}`);
  }

}
