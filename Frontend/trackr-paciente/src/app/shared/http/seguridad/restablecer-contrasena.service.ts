import { HttpClient } from '@angular/common/http';
import { Usuario } from '@models/usuario';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RestablecerContrasenaDto } from '@dtos/seguridad/restablecer-contrasena-dto';

@Injectable()
export class RestablecerContrasenaService {
  private url = 'restablecerContrasena/';

  constructor(public http: HttpClient) {}

  restablecerContrasena(usuario: RestablecerContrasenaDto):Observable<void> {
    return this.http.post<void>(this.url + 'restablecerContrasena', usuario);
  }

  validarActualizarContrasena(usuario: RestablecerContrasenaDto):Observable<boolean> {
    return this.http.post<boolean>(this.url + 'validarActualizarContrasena', usuario);
  }

  procesarActualizacionContrasena(usuario: RestablecerContrasenaDto) :Observable<void>{
    return this.http.post<void>(this.url + 'procesarActualizacionContrasena', usuario);
  }

}
