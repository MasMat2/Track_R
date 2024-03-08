import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HospitalLogotipo } from '@models/catalogo/hospital-logotipo';

@Injectable({
  providedIn: 'root'
})
export class HospitalLogotipoService {
  private dataUrl = 'hospitalLogotipo/';

  constructor(public http: HttpClient) {}

  consultarPorHospital(idHospital: number) {
    return this.http.get<HospitalLogotipo>(this.dataUrl + `consultarPorHospital/${idHospital}`);
  }

  agregar(hospitalLogotipo: HospitalLogotipo) {
    return this.http.post(this.dataUrl + 'agregar', hospitalLogotipo);
  }

  eliminar(idHospitalLogotipo: number) {
    return this.http.delete(this.dataUrl + `eliminar/${idHospitalLogotipo}`);
  }
}
