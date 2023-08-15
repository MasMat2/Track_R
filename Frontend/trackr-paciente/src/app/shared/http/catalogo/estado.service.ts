import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EstadoSelectorDto } from 'src/app/shared/dtos/catalogo/estado-selector-dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EstadoService {
  private dataUrl = 'estado/';

  constructor(public http: HttpClient ) { }

  consultarPorPaisParaSelector(idPais: number): Observable<EstadoSelectorDto[]> {
    return this.http.get<EstadoSelectorDto[]>(this.dataUrl + `selector/pais/${idPais}`);
  }

}
