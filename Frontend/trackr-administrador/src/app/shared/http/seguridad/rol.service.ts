import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Rol } from '@models/seguridad/rol';

@Injectable()
export class RolService {
  private dataUrl = 'rol/'

  constructor(private http: HttpClient) { }

  public consultarTodosParaSelector() {
    return this.http.get<Rol[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  public consultarTodosParaGrid() {
    return this.http.get<Rol[]>(this.dataUrl + 'consultarTodosParaGrid');
  }

  public consultarRolesUsuarioSesion() {
    return this.http.get<Rol[]>(this.dataUrl + 'consultarRolesUsuarioSesion');
  }

  public consultar(idRol: number) {
    return this.http.get<Rol>(this.dataUrl + `consultar/${idRol}`);
  }

  public agregar(rol: Rol) {
    return this.http.post<void>(this.dataUrl + 'agregar', rol);
  }

  public editar(rol: Rol) {
    return this.http.put<void>(this.dataUrl + 'editar', rol);
  }

  public eliminar(idRol: number) {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idRol}`);
  }
}
