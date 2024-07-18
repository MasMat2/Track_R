import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RespuestasClasificacionPreguntaGridDto } from '../../examen/respuestas-clasificacion-pregunta-grid-dto';
import { RespuestasClasificacionPreguntaInformacionGeneralDto } from '../../examen/respuestas-clasificacion-pregunta-informacion-general-dto';
import { RespuestasClasificacionPreguntaFormularioDto } from '../../examen/respuestas-clasificacion-pregunta-formulario-dto';

@Injectable({
  providedIn: 'root'
})
export class RespuestasClasificacionPreguntaService {
  private dataUrl = 'respuestasClasificacionPregunta/';
  constructor(private http: HttpClient) {}

  consultarParaGrid(idClasificacionPregunta: number): Observable<RespuestasClasificacionPreguntaGridDto[]> {
    return this.http.get<RespuestasClasificacionPreguntaGridDto[]>(this.dataUrl+`grid/${idClasificacionPregunta}`);
  }
  
  consultarParaFormulario(idRespuestasClasificacionPregunta: number): Observable<RespuestasClasificacionPreguntaInformacionGeneralDto> {
    return this.http.get<RespuestasClasificacionPreguntaInformacionGeneralDto>(this.dataUrl+`${idRespuestasClasificacionPregunta}`);
  }
  agregar(captura: RespuestasClasificacionPreguntaFormularioDto): Observable<void> {
    return this.http.post<void>(this.dataUrl+`agregar`, captura);
  }
  

  editar(captura: RespuestasClasificacionPreguntaFormularioDto): Observable<void>{
    return this.http.put<void>(this.dataUrl+`editar`,captura);
  }

  public eliminar(idRespuestasClasificacionPregunta: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `${idRespuestasClasificacionPregunta}`);
  }
}
