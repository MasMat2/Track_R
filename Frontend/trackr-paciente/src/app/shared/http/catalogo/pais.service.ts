import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaisSelectorDto } from '@models/catalogo/pais-selector-dto';

@Injectable({
  providedIn: 'root'
})
export class PaisService {
  private dataUrl = 'pais/';

  constructor(public http: HttpClient) { }


  consultarTodosParaSelector() {
    return this.http.get<PaisSelectorDto[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

}
