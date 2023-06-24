import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MunicipioFormularioCaptura } from '@dtos/catalogo/municipio-formulario-captura';
import { MunicipioFormularioConsulta } from '@dtos/catalogo/municipio-formulario-consulta';
import { MunicipioGrid } from '@dtos/catalogo/municipio-grid';
import { MunicipioSelector } from '@dtos/catalogo/municipio-selector';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MunicipioService {
  private dataUrl = 'municipio/';

  constructor(public http: HttpClient) {}

  public consultarParaFormulario(idMunicipio: number): Observable<MunicipioFormularioConsulta> {
    return this.http.get<MunicipioFormularioConsulta>(this.dataUrl + `formulario/${idMunicipio}`);
  }

  public consultarParaGrid(): Observable<MunicipioGrid[]> {
    return this.http.get<MunicipioGrid[]>(this.dataUrl + 'grid');
  }

  public consultarParaSelector(): Observable<MunicipioSelector[]> {
    return this.http.get<MunicipioSelector[]>(this.dataUrl + 'selector');
  }

  public consultarPorEstadoParaSelector(idEstado: number): Observable<MunicipioSelector[]> {
    return this.http.get<MunicipioSelector[]>(this.dataUrl + `selector/estado/${idEstado}`);
  }

  public agregar(municipio: MunicipioFormularioCaptura): Observable<void> {
    return this.http.post<void>(this.dataUrl, municipio);
  }

  public editar(municipio: MunicipioFormularioCaptura): Observable<void> {
    return this.http.put<void>(this.dataUrl, municipio);
  }

  public eliminar(idEstado: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `${idEstado}`);
  }
}
