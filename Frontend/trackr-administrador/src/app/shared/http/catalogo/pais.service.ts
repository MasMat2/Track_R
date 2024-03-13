import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Pais } from '@models/catalogo/pais';

@Injectable()
export class PaisService {
  private dataUrl = 'pais/';

  constructor(public http: HttpClient) {}

  consultarTodosParaSelector() {
    return this.http.get<Pais[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  consultar(idPais: number) {
    return this.http.get<Pais>(this.dataUrl + `consultar/${idPais}`);
  }

  consultarPorClave(clave: string) {
    return this.http.get<Pais>(this.dataUrl + `consultarPorClave/${clave}`);
  }

  consultarGeneral() {
    return this.http.get<Pais[]>(this.dataUrl + 'consultarGeneral');
  }

  agregar(pais: Pais) {
    return this.http.post<void>(this.dataUrl + 'agregar', pais);
  }

  editar(pais: Pais) {
    return this.http.put<void>(this.dataUrl + 'editar', pais);
  }

  eliminar(idPais: number) {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idPais}`);
  }
}