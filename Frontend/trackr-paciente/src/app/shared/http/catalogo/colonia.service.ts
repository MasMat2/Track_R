import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ColoniaSelectorDto } from '@models/catalogo/colonia-selector-dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ColoniaService {
  private dataUrl = 'colonia/';

  constructor(public http: HttpClient) { }


  consultarPorCodigoParaSelector(codigoPostal: string): Observable<ColoniaSelectorDto[]> {
    return this.http.get<ColoniaSelectorDto[]>(this.dataUrl + `consultarPorCodigoParaSelector/${codigoPostal}`);
  }

}
