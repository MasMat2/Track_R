import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SeccionCampo } from '@models/gestion-entidad/seccion-campo';

@Injectable()
export class SeccionCampoService {
  private dataUrl = 'seccionCampo/';

  constructor(public http: HttpClient) {}

  consultar(idSeccionCampo: number) {
    return this.http.get<SeccionCampo>(this.dataUrl + `consultar/${idSeccionCampo}`);
  }
  consultarPorSeccion(idSeccion: number) {
    return this.http.get<any[]>(this.dataUrl + `consultarPorSeccion/${idSeccion}`);
  }
  agregar(seccionCampo: SeccionCampo) {
    return this.http.post<void>(this.dataUrl + 'agregar', seccionCampo);
  }
  editar(seccionCampo: SeccionCampo) {
    return this.http.put<void>(this.dataUrl + 'editar', seccionCampo);
  }
  eliminar(idSeccionCampo: number) {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idSeccionCampo}`);
  }
}
