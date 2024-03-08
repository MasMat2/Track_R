import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Entidad } from '@models/gestion-entidad/entidad';

@Injectable()
export class EntidadService {
  private dataUrl = 'entidad/';

  constructor(public http: HttpClient) {}


  consultar(idEntidad: number) {
    return this.http.get<Entidad>(this.dataUrl + `consultar/${idEntidad}`);
  }

  consultarPorClave(clave: string) {
    return this.http.get<Entidad>(this.dataUrl + `consultarPorClave/${clave}`);
  }


  consultarTodosParaGrid(): Observable<Entidad[]> {
    return this.http.get<Entidad[]>(this.dataUrl + 'consultarTodosParaGrid');
  }

  agregar(expediente: Entidad) {
    return this.http.post<number>(this.dataUrl + 'agregar', expediente);
  }

  editar(pais: Entidad) {
    return this.http.put<void>(this.dataUrl + 'editar', pais);
  }

  eliminar(idEntidad: number) {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idEntidad}`);
  }

  actualizarExpedienteTrackr(): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'actualizarExpedienteTrackr', null);
  }
}
