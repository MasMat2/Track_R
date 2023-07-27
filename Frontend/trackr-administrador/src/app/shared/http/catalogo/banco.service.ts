import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Banco } from '@models/catalogo/banco';

@Injectable({
  providedIn: 'root'
})
export class BancoService {
  private dataUrl = 'banco/';

  constructor(private http: HttpClient) { }

  public consultarTodosParaSelector() {
    return this.http.get<Banco[]>(this.dataUrl + 'consultarTodosParaSelector');
  }
}
