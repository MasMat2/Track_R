import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TipoUsuario } from '@models/seguridad/tipo-usuario';

@Injectable({
  providedIn: 'root'
})
export class TipoUsuarioService {
  private url = 'tipoUsuario/';

  constructor(public http: HttpClient) {}

  consultarTipoAdministrador() {
    return this.http.get<TipoUsuario>(this.url + `consultarTipoAdministrador`);
  }

  consultarTiposUsuarioSelector() {
    return this.http.get<TipoUsuario[]>(this.url + `consultarTiposUsuarioSelector`);
  }
}
