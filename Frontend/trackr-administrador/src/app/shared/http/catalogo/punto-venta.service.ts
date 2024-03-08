import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PuntoVenta } from '@models/catalogo/punto-venta';
import { Usuario } from '@models/seguridad/usuario';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PuntoVentaService {
  private dataUrl = 'puntoVenta/';

  constructor(public http: HttpClient) {}

 public consultarTodosParaGrid(): Observable<PuntoVenta[]> {
    return this.http.get<PuntoVenta[]>(this.dataUrl + 'consultarTodosParaGrid');
  }

  public consultarTodosParaSelector(): Observable<PuntoVenta[]> {
    return this.http.get<PuntoVenta[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  public consultarPorUsuarioEnSesion(): Observable<PuntoVenta> {
    return this.http.get<PuntoVenta>(this.dataUrl + 'consultarPorUsuarioEnSesion');
  }

  public consultar(idPuntoVenta: number): Observable<PuntoVenta> {
    return this.http.get<PuntoVenta>(this.dataUrl + `consultar/${idPuntoVenta}`);
  }

  public consultarUsuariosAsignados(idPuntoVenta: number): Observable<Usuario[]> {
    return this.http.get<Usuario[]>(this.dataUrl + `consultarUsuariosAsignados/${idPuntoVenta}`);
  }

  public agregar(puntoVenta: PuntoVenta): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'agregar', puntoVenta);
  }

  public editar(puntoVenta: PuntoVenta): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', puntoVenta);
  }

  public eliminar(idPuntoVenta: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idPuntoVenta}`);
  }

}
