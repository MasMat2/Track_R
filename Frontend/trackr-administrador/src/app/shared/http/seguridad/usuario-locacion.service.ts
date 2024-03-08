import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UsuarioLocacion } from '@models/seguridad/usuario-locacion';

@Injectable()
export class UsuarioLocacionService {
  private dataUrl = 'usuarioLocacion/'

  constructor(private http: HttpClient) { }

  public consultarPorUsuario(idUsuario: number) {
    return this.http.get<UsuarioLocacion[]>(this.dataUrl + `consultarPorUsuario/${idUsuario}`);
  }

  public consultar(idRol: number) {
    return this.http.get<UsuarioLocacion>(this.dataUrl + `consultar/${idRol}`);
  }

  public agregar(usuarioLocacion: UsuarioLocacion) {
    return this.http.post<void>(this.dataUrl + 'agregar', usuarioLocacion);
  }

  public editar(usuarioLocacion: UsuarioLocacion) {
    return this.http.put<void>(this.dataUrl + 'editar', usuarioLocacion);
  }

  public eliminar(idUsuarioLocacion: number) {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idUsuarioLocacion}`);
  }
}
