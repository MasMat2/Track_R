import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UsuarioRol } from '@models/seguridad/usuario-rol';

@Injectable()
export class UsuarioRolService {
  private dataUrl = 'usuarioRol/'

  constructor(private http: HttpClient) { }

  consultarPorUsuario(idUsuario: number) {
    return this.http.get<UsuarioRol[]>(this.dataUrl + `consultarPorUsuario/${idUsuario}`);
  }

  consultarPorUsuarioParaGrid(idUsuario: number) {
    return this.http.get<UsuarioRol[]>(this.dataUrl + `consultarPorUsuarioParaGrid/${idUsuario}`);
  }

  guardar(listaPrecio: any) {
    return this.http.post<void>(this.dataUrl + 'guardar', listaPrecio);
  }

  agregar(usuarioRol: UsuarioRol) {
    return this.http.post<void>(this.dataUrl + 'agregar', usuarioRol);
  }

  editar(usuarioRol: UsuarioRol) {
    return this.http.put<void>(this.dataUrl + 'editar', usuarioRol);
  }

    eliminar(idUsuarioRol: number) {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idUsuarioRol}`);
  }


}
