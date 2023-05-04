import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AgrupadorCuentaContable } from '@models/contabilidad/agrupador-cuenta-contable';

@Injectable()
export class AgrupadorCuentaContableService {
  private dataUrl = 'agrupadorCuentaContable/';

  constructor(private http: HttpClient) { }

  public consultarParaGrid(): Observable<AgrupadorCuentaContable[]> {
    return this.http.get<AgrupadorCuentaContable[]>(this.dataUrl + `consultarParaGrid`);
  }

  public consultarParaSelector(): Observable<AgrupadorCuentaContable[]> {
    return this.http.get<AgrupadorCuentaContable[]>(this.dataUrl + `consultarParaSelector`);
  }

  public consultarDto(idAgrupador: number): Observable<AgrupadorCuentaContable> {
    return this.http.get<AgrupadorCuentaContable>(this.dataUrl + `consultarDto/${idAgrupador}`);
  }

  public agregar(agrupador: AgrupadorCuentaContable): Observable<void> {
    return this.http.post<void>(this.dataUrl + `agregar`, agrupador);
  }

  public editar(agrupador: AgrupadorCuentaContable): Observable<void> {
    return this.http.put<void>(this.dataUrl + `editar`, agrupador);
  }

  public eliminar(idAgrupador: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idAgrupador}`);
  }
}