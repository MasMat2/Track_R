import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegimenFiscal } from '@models/catalogo/regimen-fiscal';

@Injectable()
export class RegimenFiscalService {
  private dataUrl = 'regimenFiscal/';

  constructor(private http: HttpClient) { }

  public consultarTodosParaSelector() {
    return this.http.get<RegimenFiscal[]>(this.dataUrl + 'consultarTodosParaSelector');
  }
}
