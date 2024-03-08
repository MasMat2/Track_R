import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Estado } from '@models/catalogo/estado';
import { EstadoFormularioConsultaDto } from '@dtos/catalogo/estado-formulario-consulta-dto';
import { EstadoGridDto } from '@dtos/catalogo/estado-grid-dto';
import { EstadoSelectorDto } from '@dtos/catalogo/estado-selector-dto';
import { EstadoFormularioCapturaDto } from '@dtos/catalogo/estado-formulario-captura-dto';

@Injectable({
  providedIn: 'root',
})
export class EstadoService {
  private dataUrl = 'estado/';

  constructor(private http: HttpClient) {}

  consultar(idEstado: number): Observable<Estado> {
    return this.http.get<Estado>(this.dataUrl + `${idEstado}`);
  }

  consultarParaFormulario(idEstado: number): Observable<EstadoFormularioConsultaDto> {
    return this.http.get<EstadoFormularioConsultaDto>(this.dataUrl + `formulario/${idEstado}`);
  }

  consultarParaGrid(): Observable<EstadoGridDto[]> {
    return this.http.get<EstadoGridDto[]>(this.dataUrl + 'grid');
  }

  consultarPorPaisParaSelector(idPais: number): Observable<EstadoSelectorDto[]> {
    return this.http.get<EstadoSelectorDto[]>(this.dataUrl + `selector/pais/${idPais}`);
  }

  agregar(estado: EstadoFormularioCapturaDto): Observable<void> {
    return this.http.post<void>(this.dataUrl, estado);
  }

  editar(estado: EstadoFormularioCapturaDto): Observable<void> {
    return this.http.put<void>(this.dataUrl, estado);
  }

  eliminar(idEstado: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `${idEstado}`);
  }
}
