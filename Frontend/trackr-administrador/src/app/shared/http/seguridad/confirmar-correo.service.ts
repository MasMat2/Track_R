import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {ConfirmarCorreoDto} from '@dtos/seguridad/confirmar-correo-dto'

@Injectable({
  providedIn: 'root'
})
export class ConfirmarCorreoService {
  private dataUrl = 'confirmacionCorreo/';

  constructor(public http: HttpClient) { }

  public validarCorreo(confirmarCorreoDto: ConfirmarCorreoDto) {
    return this.http.post<boolean>(this.dataUrl + 'validarCorreo', confirmarCorreoDto);
  }


}
