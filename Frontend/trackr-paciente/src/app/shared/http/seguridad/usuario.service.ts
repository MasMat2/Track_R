import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { InformacionGeneralDto } from 'src/app/shared/dtos/perfil/informacion-general-dto';
import { Usuario } from '@models/usuario';
import { Observable } from 'rxjs';
import { UsuarioNuevoTrackrDto } from '../../Dtos/seguridad/usuario-nuevo-trackr-dto';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private url = 'usuario/';

  constructor(public http: HttpClient) { }

  agregar(usuario: Usuario) {
    return this.http.post<number>(this.url + 'agregar', usuario);
  }

  agregarTrackr(usuarioDto: UsuarioNuevoTrackrDto) {
    return this.http.post<number>(this.url + 'agregarTrackr', usuarioDto);
  }

  consultarInformacionGeneral(): Observable<InformacionGeneralDto>{
    return this.http.get<InformacionGeneralDto>(this.url + 'consultarInformacionGeneral');
  }

  actualizarInformacionGeneral(informacion: InformacionGeneralDto){
    return this.http.put(this.url + 'actualizarInformacionGeneral', informacion);
  }

}
