import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lada } from '@models/catalogo/lada';

@Injectable()
export class LadaService {
  private dataUrl = 'lada/';

  constructor(private http: HttpClient) { }

  public consultarTodosParaSelector() {
    return this.http.get<Lada[]>(this.dataUrl + 'consultarTodosParaSelector');
  }
}
