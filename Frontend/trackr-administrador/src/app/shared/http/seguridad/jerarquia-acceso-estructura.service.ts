import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { JerarquiaEstructuraArbol } from '@models/contabilidad/jerarquia-estructura-arbol';
import { JerarquiaAccesoEstructura } from '@models/seguridad/jerarquia-acceso-estructura';

@Injectable()
export class JerarquiaAccesoEstructuraService {
  private dataUrl = 'jerarquiaAccesoEstructura/';
  constructor(private http: HttpClient) { }

  public consultarPorJerarquia(idJerarquia: number): Observable<JerarquiaEstructuraArbol[]> {
    return this.http.get<JerarquiaEstructuraArbol[]>(this.dataUrl + `consultarPorJerarquia/${idJerarquia}`);
  }

  public consultarPorJerarquiaParaSelector(idJerarquia: number): Observable<JerarquiaEstructuraArbol[]> {
    return this.http.get<JerarquiaEstructuraArbol[]>(this.dataUrl + `consultarPorJerarquiaParaSelector/${idJerarquia}`);
  }
  
  public consultarArbol(idJerarquia: number): Observable<JerarquiaEstructuraArbol[]> {
    return this.http.get<JerarquiaEstructuraArbol[]>(this.dataUrl + `consultarArbol/${idJerarquia}`);
  }

  public agregar(jerarquias: JerarquiaAccesoEstructura[]): Observable<void> {
    return this.http.post<void>(this.dataUrl + `agregar`, jerarquias);
  }

  public eliminar(idJerarquiaAccesoEstructura: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idJerarquiaAccesoEstructura}`);
  }
}