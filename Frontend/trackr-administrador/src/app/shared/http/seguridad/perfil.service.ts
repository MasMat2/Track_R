import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Perfil } from '@models/seguridad/perfil';

@Injectable()
export class PerfilService {
  private dataUrl = 'perfil/';

  constructor(public http: HttpClient) {}

  public consultar(idPerfil: number): Observable<Perfil> {
    return this.http.get<Perfil>(this.dataUrl + `consultar/${idPerfil}`);
  }

  public consultarPorCompania(): Observable<Perfil[]> {
    return this.http.get<Perfil[]>(this.dataUrl + `consultarPorCompania`);
  }

  public consultarPorCompaniaParaSelector(idCompania: number) : Observable<Perfil[]> {
    return this.http.get<Perfil[]>(this.dataUrl + `consultarPorCompaniaParaSelector/${idCompania}`);
  }

  public consultarTodosParaSelector(): Observable<Perfil[]> {
    return this.http.get<Perfil[]>(this.dataUrl + 'consultarTodosParaSelector');
  }
  public agregar(perfil: Perfil): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'agregar', perfil);
  }

  public editar(perfil: Perfil): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', perfil);
  }

  public eliminar(idPerfil: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idPerfil}`);
  }
}
