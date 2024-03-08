import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Icono } from '@models/catalogo/icono';

@Injectable()
export class IconoService {
  private dataUrl = 'icono/';

  constructor(public http: HttpClient) {}

  public consultarGeneral(): Observable<Icono[]> {
    return this.http.get<Icono[]>(this.dataUrl + 'consultarGeneral');
  }
}
