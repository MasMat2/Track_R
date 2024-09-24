import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RespuestasExcelDto } from '@dtos/gestion-examen/respuestas-excel-dto';
import { ExamenReactivo } from '@models/examen/examen-reactivo';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExamenReactivoService {
  private dataUrl = 'examenReactivo/';

  constructor(public http: HttpClient) {}

  public consultarTodosParaSelector(): Observable<ExamenReactivo[]> {
    return this.http.get<ExamenReactivo[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  public consultar(idExamenReactivo: number): Observable<ExamenReactivo> {
    return this.http.get<ExamenReactivo>(this.dataUrl + `consultar/${idExamenReactivo}`);
  }

  public consultarGeneral(idExamen: number): Observable<ExamenReactivo[]> {
    return this.http.get<ExamenReactivo[]>(this.dataUrl + `consultarGeneral/${idExamen}`);
  }

  public agregar(examenReactivo: ExamenReactivo): Observable<number> {
    return this.http.post<number>(this.dataUrl + 'agregar', examenReactivo);
  }

  public editar(examenReactivo: ExamenReactivo): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', examenReactivo);
  }

  public eliminar(idExamenReactivo: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idExamenReactivo}`);
  }

  public consultarReactivosExamen(idExamen: number): Observable<ExamenReactivo[]> {
    return this.http.get<ExamenReactivo[]>(this.dataUrl + `consultarReactivosExamen/${idExamen}`);
  }

  public revisar(examenReactivoList: ExamenReactivo[]): Observable<number> {
    return this.http.post<number>(this.dataUrl + 'revisar', examenReactivoList);
  }

  public consultarReactivosExamenParaExcel(idProgramacionExamen: number): Observable<RespuestasExcelDto>{
    return this.http.get<RespuestasExcelDto>(this.dataUrl + `consultarReactivosExamenParaExcel/${idProgramacionExamen}`);
  }
}
