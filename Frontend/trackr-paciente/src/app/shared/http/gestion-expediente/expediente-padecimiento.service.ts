import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AgregarExpedientePadecimientoDto } from '../../Dtos/seguridad/agregar-expediente-padecimiento-dto';
import { ExpedientePadecimientoSelectorDTO } from '../../Dtos/seguridad/expediente-padecimiento-selector-dto copy';

@Injectable({
  providedIn: 'root',
})
export class ExpedientePadecimientoService {
  private dataUrl = 'expedientePadecimiento/';

  constructor(public http: HttpClient) {}


  public consultarPorUsuarioParaSelector(): Observable<ExpedientePadecimientoSelectorDTO[]>{
    return this.http.get<ExpedientePadecimientoSelectorDTO[]>(this.dataUrl + 'consultarPorUsuarioSelector');
  }

  public eliminar(idExpedientePadecimiento: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `${idExpedientePadecimiento}`);
  }

  public agregarPadecimiento(agregarExpedientePadecimientoDto: AgregarExpedientePadecimientoDto): Observable<number> {
    return this.http.post<number>(this.dataUrl + 'agregarExpedientePadecimiento', agregarExpedientePadecimientoDto);
  }

}
