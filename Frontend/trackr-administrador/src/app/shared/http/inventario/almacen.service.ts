import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Almacen } from '@models/inventario/almacen';

@Injectable()
export class AlmacenService {
  private dataUrl = 'almacen/';

  constructor(public http: HttpClient) {}

  consultar(idAlmacen: number) {
    return this.http.get<Almacen>(this.dataUrl + `consultar/${idAlmacen}`);
  }

  consultarGeneral() {
    return this.http.get<Almacen[]>(this.dataUrl + 'consultarGeneral');
  }
  consultarTodosParaSelector() {
    return this.http.get<Almacen[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  consultarPorEstado(idEstado: number) {
    return this.http.get<Almacen[]>(this.dataUrl + `consultarPorEstado/${idEstado}`);
  }

  consultarPorRol(idResponsable: number) {
    return this.http.get<Almacen[]>(this.dataUrl + `consultarPorUsuario/${idResponsable}`);
  }

  consultarPorEstatus(idEstatus: number) {
    return this.http.get<Almacen[]>(this.dataUrl + `consultarPorEstatus/${idEstatus}`);
  }

  consultarPorCompania() {
    return this.http.get<Almacen[]>(this.dataUrl + 'consultarPorCompania');
  }

  agregar(almacen: Almacen) {
    return this.http.post<number>(this.dataUrl + 'agregar', almacen);
  }

  editar(almacen: Almacen) {
    return this.http.put<void>(this.dataUrl + 'editar', almacen);
  }

  eliminar(idAlmacen: number) {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idAlmacen}`);
  }
}
