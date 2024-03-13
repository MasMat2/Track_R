import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ExpedientePadecimientoSelectorDTO } from '@dtos/seguridad/expediente-padecimiento-selector-dto';
import { EntidadEstructura } from '@models/gestion-entidad/entidad-estructura';
import { Observable } from 'rxjs';

@Injectable()
export class EntidadEstructuraService {
  private dataUrl = 'entidadEstructura/';
  constructor(private http: HttpClient) { }

  public consultarArbol(idEntidad: number): Observable<EntidadEstructura[]> {
    return this.http.get<EntidadEstructura[]>(this.dataUrl + `consultarArbol/${idEntidad}`);
  }

  public consultarParaTabulador(idEntidad: number): Observable<EntidadEstructura[]> {
    return this.http.get<EntidadEstructura[]>(this.dataUrl + `tabulador/${idEntidad}`);
  }

  public consultarPorEntidadParaSelector(idEntidad: number): Observable<EntidadEstructura[]> {
    return this.http.get<EntidadEstructura[]>(this.dataUrl + `consultarPorEntidadParaSelector/${idEntidad}`);
  }

  public agregar(entidadEstructuras: EntidadEstructura[]): Observable<void> {
    return this.http.post<void>(this.dataUrl + `agregar`, entidadEstructuras);
  }

  public eliminar(idEntidadEstructura: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idEntidadEstructura}`);
  }

  public consultarPadecimientosParaSelector(): Observable<ExpedientePadecimientoSelectorDTO[]> {
    return this.http.get<ExpedientePadecimientoSelectorDTO[]>(this.dataUrl + `consultarPadecimientosParaSelector/`);
  }

}
