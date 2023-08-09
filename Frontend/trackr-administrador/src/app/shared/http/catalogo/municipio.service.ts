import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MunicipioFormularioCapturaDto } from '@dtos/catalogo/municipio-formulario-captura-dto';
import { MunicipioFormularioConsultaDto } from '@dtos/catalogo/municipio-formulario-consulta-dto';
import { MunicipioGridDto } from '@dtos/catalogo/municipio-grid-dto';
import { MunicipioSelectorDto } from '@dtos/catalogo/municipio-selector-dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MunicipioService {
  private dataUrl = 'municipio/';

  constructor(public http: HttpClient) {}

  public consultarParaFormulario(idMunicipio: number): Observable<MunicipioFormularioConsultaDto> {
    return this.http.get<MunicipioFormularioConsultaDto>(this.dataUrl + `formulario/${idMunicipio}`);
  }

  public consultarParaGrid(): Observable<MunicipioGridDto[]> {
    return this.http.get<MunicipioGridDto[]>(this.dataUrl + 'grid');
  }

  public consultarParaSelector(): Observable<MunicipioSelectorDto[]> {
    return this.http.get<MunicipioSelectorDto[]>(this.dataUrl + 'selector');
  }

  public consultarPorEstadoParaSelector(idEstado: number): Observable<MunicipioSelectorDto[]> {
    return this.http.get<MunicipioSelectorDto[]>(this.dataUrl + `selector/estado/${idEstado}`);
  }

  public agregar(municipio: MunicipioFormularioCapturaDto): Observable<void> {
    return this.http.post<void>(this.dataUrl, municipio);
  }

  public editar(municipio: MunicipioFormularioCapturaDto): Observable<void> {
    return this.http.put<void>(this.dataUrl, municipio);
  }

  public eliminar(idEstado: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `${idEstado}`);
  }
}
