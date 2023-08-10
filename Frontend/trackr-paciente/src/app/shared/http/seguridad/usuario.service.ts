import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { InformacionGeneralDto } from '@models/perfil/informacion-general-dto';
import { Usuario } from '@models/usuario';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private url = 'usuario/';

  constructor(public http: HttpClient) { }

  agregar(usuario: Usuario) {
    return this.http.post<number>(this.url + 'agregar', usuario);
  }

  consultarInformacionGeneral(): Observable<InformacionGeneralDto>{
    return this.http.get<InformacionGeneralDto>(this.url + 'consultarInformacionGeneral');
  }

  actualizarInformacionGeneral(informacion: InformacionGeneralDto): void{
    this.http.put(this.url + 'actualizarInformacionGeneral', informacion);
  }

}
