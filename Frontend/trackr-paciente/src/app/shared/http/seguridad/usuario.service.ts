import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Usuario } from '@models/usuario';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private url = 'usuario/';

  constructor(public http: HttpClient) { }

  agregar(usuario: Usuario) {
    return this.http.post<number>(this.url + 'agregar', usuario);
  }

}
