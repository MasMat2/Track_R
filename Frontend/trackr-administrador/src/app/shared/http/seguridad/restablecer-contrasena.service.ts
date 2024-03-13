import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RestablecerContrasenaDto } from '@dtos/seguridad/restablecer-contrasena-dto';

@Injectable({
  providedIn: 'root'
})
export class RestablecerContrasenaService {
  dataUrl = 'restablecerContrasena/'

  constructor(private http: HttpClient) { }

  public validarActualizarContrasena(restablecerContrasena: RestablecerContrasenaDto) {
    return this.http.post<boolean>(this.dataUrl + 'validarActualizarContrasena', restablecerContrasena);
  }

  public procesarActualizarContrasena(restablecerContrasena: RestablecerContrasenaDto) {
    return this.http.post<void>(this.dataUrl + 'procesarActualizacionContrasena', restablecerContrasena);
  }

  

}
