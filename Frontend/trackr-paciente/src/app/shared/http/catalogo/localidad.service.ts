import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LocalidadSelectorDto } from 'src/app/shared/dtos/catalogo/localidad-selector-dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocalidadService {
  private dataUrl = 'localidad/';
  constructor(public http: HttpClient) { }

  consultarPorEstado(idEstado: number): Observable<LocalidadSelectorDto[]> {
    return this.http.get<LocalidadSelectorDto[]>(this.dataUrl + `consultarPorEstado/${idEstado}`);
  }
  
}
