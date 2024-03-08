import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UsuarioDto } from 'src/app/shared/Dtos/perfil/usuario-dto';

@Injectable({
  providedIn: 'root',
})
export class UsuarioService {
  private dataUrl = 'usuario/';

  constructor(public http: HttpClient) {}

  public consultarMiUsuario(): Observable<UsuarioDto> {
    return this.http.get<UsuarioDto>(this.dataUrl + `consultarMiUsuario`);
  }

}
