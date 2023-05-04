import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CompaniaLogotipo } from '@models/catalogo/compania-logotipo';

@Injectable({
  providedIn: 'root'
})
export class CompaniaLogotipoService {
  private dataUrl = 'companiaLogotipo/';

  constructor(public http: HttpClient) {}

  public consultarPorCompania(idCompania: number) {
    return this.http.get<CompaniaLogotipo>(this.dataUrl + `consultarPorCompania/${idCompania}`);
  }

  public agregar(companiaLogotipo: CompaniaLogotipo) {
    return this.http.post(this.dataUrl + 'agregar', companiaLogotipo);
  }

  public eliminar(idCompaniaLogotipo: number) {
    return this.http.delete(this.dataUrl + `eliminar/${idCompaniaLogotipo}`);
  }
}
