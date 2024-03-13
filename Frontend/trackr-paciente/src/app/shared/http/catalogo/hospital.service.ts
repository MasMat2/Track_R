import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Hospital } from '@models/catalogo/hospital';

@Injectable({providedIn:'root'})
export class HospitalService {
  private dataUrl = 'hospital/';

  constructor(public http: HttpClient) {}


  consultarTodosParaSelector() {
    return this.http.get<Hospital[]>(this.dataUrl + 'consultarTodosParaSelector');
  }
}
