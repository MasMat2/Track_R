import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ExpedientePadecimientoSelectorDTO } from '../../dtos/seguridad/expediente-padecimiento-selector-dto';

@Injectable({
  providedIn: 'root'
})
export class EntidadEstructuraService {

  private dataUrl = 'entidadEstructura/';

  constructor(private http: HttpClient) { }

  public consultarPadecimientosParaSelector(): Observable<ExpedientePadecimientoSelectorDTO[]> {
    return this.http.get<ExpedientePadecimientoSelectorDTO[]>(this.dataUrl + `consultarPadecimientosParaSelector/`);
  }

  public consultarDiagnosticosParaSelector(): Observable<ExpedientePadecimientoSelectorDTO[]> {
    return this.http.get<ExpedientePadecimientoSelectorDTO[]>(this.dataUrl + `diagnosticos/selector`);
  }

  public consultarAntecedentesParaSelector(): Observable<ExpedientePadecimientoSelectorDTO[]> {
    return this.http.get<ExpedientePadecimientoSelectorDTO[]>(this.dataUrl + `antecedentes/selector`);
  }
}
