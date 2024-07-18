import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Especialidad } from '@models/catalogo/especialidad';
import { EspecialidadFormularioConsultaDto } from '@dtos/catalogo/especialidad-formulario-consulta-dto';
import { EspecialidadGridDto } from '@dtos/catalogo/especialidad-grid-dto';
import { EspecialidadFormularioCapturaDto } from '@dtos/catalogo/especialidad-formulario-captura-dto';

@Injectable({
  providedIn: 'root',
})
export class EspecialidadService {
  private dataUrl = 'especialidad/';

  constructor(private http: HttpClient) {}

  consultarParaFormulario(idEspecialidad: number): Observable<EspecialidadFormularioConsultaDto> {
    return this.http.get<EspecialidadFormularioConsultaDto>(this.dataUrl + `formulario/${idEspecialidad}`);
  }

  consultarParaGrid(): Observable<EspecialidadGridDto[]> {
    return this.http.get<EspecialidadGridDto[]>(this.dataUrl + 'grid');
  }

  agregar(especialidad: EspecialidadFormularioCapturaDto): Observable<void>
  {
    return this.http.post<void>(this.dataUrl, especialidad);
  } 

  /* agregar(especialidad: EspecialidadFormularioCapturaDto): Observable<number>
  {
    return this.http.post<number>(this.dataUrl, 35);
  } */
  
  editar(especialidad: EspecialidadFormularioCapturaDto): Observable<void> {
    return this.http.put<void>(this.dataUrl, especialidad);
  }

  eliminar(idEspecialidad: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `${idEspecialidad}`);
  }
}