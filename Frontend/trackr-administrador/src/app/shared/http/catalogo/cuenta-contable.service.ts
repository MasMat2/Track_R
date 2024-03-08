import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CuentaContable } from '@models/catalogo/cuenta-contable';
import { ArchivoExcel } from '@models/gestion-egresos/archivo-excel';
import { Observable } from 'rxjs';

@Injectable()
export class CuentaContableService{
  private dataUrl = 'cuentaContable/';

  constructor(public http: HttpClient) {}

  consultar(idCuentaContable: number) {
    return this.http.get<CuentaContable>(this.dataUrl + `consultar/${idCuentaContable}`);
  }

  consultarConPartidasAbiertas() {
    return this.http.get<CuentaContable[]>(this.dataUrl + 'consultarConPartidasAbiertas');
  }

  consultarTodosParaSelector() {
    return this.http.get<CuentaContable[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  consultarPorAgrupadorParaSelector(idAgrupador: number) {
    return this.http.get<CuentaContable[]>(this.dataUrl + `consultarPorAgrupadorParaSelector/${idAgrupador}`)
  }
  ConsultarPorAgrupadorParaGrid(idAgrupador: number) {
    return this.http.get<CuentaContable[]>(this.dataUrl + `consultarPorAgrupadorParaGrid/${idAgrupador}`)
  }

  agregar(cuentaContable: CuentaContable) {
    return this.http.post<void>(this.dataUrl + 'agregar', cuentaContable);
  }

  editar(cuentaContable: CuentaContable) {
    return this.http.put<void>(this.dataUrl + 'editar', cuentaContable);
  }

  eliminar(idCuentaContable: number) {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idCuentaContable}`);
  }

  public consultarPorFiltroParaGrid(filtro: any) {
    return this.http.post<CuentaContable[]>(this.dataUrl + 'consultarPorFiltroParaGrid',filtro);
  }

  public consultarTodosParaGrid() {
    return this.http.get<CuentaContable[]>(this.dataUrl + 'consultarTodosParaGrid');
  }

  public consultarParaJerarquiaGrid(idJerarquia: number): Observable<CuentaContable[]> {
    return this.http.get<CuentaContable[]>(this.dataUrl + `consultarParaJerarquiaGrid/${idJerarquia}`);
  }

  public cargarCuentas(archivo: ArchivoExcel): Observable<void> {
    return this.http.post<void>(this.dataUrl + `cargarCuentas`, archivo);
  }


   // consultarPorSubtipoCuentaContable(idSubtipoCuentaContable: number) {
  //   return this.http.get<CuentaContable[]>(this.dataUrl + `consultarPorSubtipoCuentaContable/${idSubtipoCuentaContable}`);
  // }

  // consultarPorTipoCuentaContable(idTipoCuentaContable: number) {
  //   return this.http.get<CuentaContable[]>(this.dataUrl + `consultarPorTipoCuentaContable/${idTipoCuentaContable}`);
  // }

}
