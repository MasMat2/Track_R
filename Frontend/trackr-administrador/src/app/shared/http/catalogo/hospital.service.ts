import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Hospital } from '@models/catalogo/hospital';

@Injectable()
export class HospitalService {
  private dataUrl = 'hospital/';

  constructor(public http: HttpClient) {}

  consultarPorCompaniaParaGrid() {
    return this.http.get<Hospital[]>(this.dataUrl + `consultarPorCompaniaParaGrid`);
  }

  consultarGeneral() {
    return this.http.get<Hospital[]>(this.dataUrl + `consultarGeneral`);
  }

  consultarPorUsuarioSesion() {
    return this.http.get<Hospital>(this.dataUrl + `consultarPorUsuarioSesion`);
  }

  consultarInformacionDeposito(idHospital: number) {
    return this.http.get<Hospital>(this.dataUrl + `consultarInformacionDeposito/${idHospital}`);
  }

  consultarPorCompania(idCompania: number) {
    return this.http.get<Hospital[]>(this.dataUrl + `consultarPorCompania/${idCompania}`);
  }

  consultar(idHospital: number) {
    return this.http.get<Hospital>(this.dataUrl + `consultar/${idHospital}`);
  }

  agregar(Hospital: Hospital) {
    return this.http.post<number>(this.dataUrl + 'agregar', Hospital);
  }

  editar(Hospital: Hospital) {
    return this.http.put<void>(this.dataUrl + 'editar', Hospital);
  }

  eliminar(idHospital: number) {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idHospital}`);
  }

  consultarConfiguracionesGenerales() {
    return this.http.get<Hospital[]>(this.dataUrl + `consultarConfiguracionesGenerales`);
  }

  EditarConfiguracionesGenerales(Hospital: Hospital) {
    return this.http.put<void>(this.dataUrl + 'EditarConfiguracionesGenerales', Hospital);
  }

  consultarAplicadoEncuesta(idEncuesta: number) {
    return this.http.get<Hospital>(this.dataUrl + `consultarAplicadoEncuesta/${idEncuesta}`);
  }

  consultarHospitalesPorUsuario() {
    return this.http.get<Hospital[]>(this.dataUrl + 'consultarHospitalesPorUsuario');
  }

  consultarTodosParaSelector() {
    return this.http.get<Hospital[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  consultarDisponiblesParaListaPrecio(idListaPrecioSeleccionada: number) {
    return this.http.get<Hospital[]>(this.dataUrl + `consultarDisponiblesParaListaPrecio/${idListaPrecioSeleccionada}`);
  }

  consultarPorCompaniaSesion() {
    return this.http.get<Hospital[]>(this.dataUrl + 'consultarPorCompaniaSesion');
  }
}
