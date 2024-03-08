import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EntidadEstructuraValor } from '@models/gestion-entidad/entidad-estructura-valor';

@Injectable()
export class EntidadEstructuraValorService {
  private dataUrl = 'entidadEstructuraValor/';

  constructor(public http: HttpClient) {}

  consultarPorTabulacion(idEntidadEstructura: number, idTabla: number) {
    return this.http.get<EntidadEstructuraValor[]>(this.dataUrl + `consultarPorTabulacion/${idEntidadEstructura},${idTabla}`);
  }

  guardar(entidadEstructuraValorList: any[]) {
    return this.http.post<any[]>(this.dataUrl + `guardar`, entidadEstructuraValorList);
  }

}
