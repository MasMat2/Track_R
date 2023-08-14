import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { municipioSelectorDto } from 'src/app/shared/dtos/catalogo/municipio-selector-dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MunicipioService {
  private dataUrl = 'municipio/';

  constructor(public http: HttpClient) { }

  consultarPorEstadoParaSelector(idEstado: number): Observable<municipioSelectorDto[]> {
    return this.http.get<municipioSelectorDto[]>(this.dataUrl + `consultarPorEstadoParaSelector/${idEstado}`);
    }
}
