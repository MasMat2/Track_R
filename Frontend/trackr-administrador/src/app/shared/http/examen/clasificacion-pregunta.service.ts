import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SimpleSelectorDto } from '@dtos/general/simple-selector-dto';
import { ClasificacionPreguntaFormularioDto } from '../../examen/clasificacion-pregunta-formulario-dto';
import { ClasificacionPreguntaGridDto } from '../../examen/clasificacion-pregunta-grid-dto';

@Injectable({
  providedIn: 'root'
})
export class ClasificacionPreguntaService {
  private dataUrl = 'clasificacionPregunta/';
  constructor(private http: HttpClient) {}

  public consultar(idClasificacionPregunta: number): Observable<ClasificacionPreguntaFormularioDto> {
    return this.http.get<ClasificacionPreguntaFormularioDto>(
      this.dataUrl + `${idClasificacionPregunta}`
    );
  }

  public consultarParaGrid(): Observable<ClasificacionPreguntaGridDto[]> {
    return this.http.get<ClasificacionPreguntaGridDto[]>(this.dataUrl);
  }

  public agregar(entidad:ClasificacionPreguntaGridDto): Observable<void> {
    return this.http.post<void>(this.dataUrl, entidad);
  }

  public editar(entidad: ClasificacionPreguntaGridDto): Observable<void> {
    return this.http.put<void>(this.dataUrl, entidad);
  }

  public eliminar(idClasificacionPregunta: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `${idClasificacionPregunta}`);
  }

    public consultarSelector(): Observable<SimpleSelectorDto[]> {
        return this.http.get<SimpleSelectorDto[]>(this.dataUrl + `consultarSelector/`);
    }

}