import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Examen } from '@models/examen/examen';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ExamenService {
  private dataUrl = 'examen/';

  constructor(private http: HttpClient) {}

  public consultarTodosParaSelector(): Observable<Examen[]> {
    return this.http.get<Examen[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  public consultar(idExamen: number): Observable<Examen> {
    return this.http.get<Examen>(this.dataUrl + `consultar/${idExamen}`);
  }

  public consultarGeneral(idProgramacionExamen: number): Observable<Examen[]> {
    return this.http.get<Examen[]>(this.dataUrl + `consultarGeneral/${idProgramacionExamen}`);
  }

  public agregar(examen: Examen): Observable<number> {
    return this.http.post<number>(this.dataUrl + 'agregar', examen);
  }

  public editar(examen: Examen): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', examen);
  }

  public eliminar(idExamen: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idExamen}`);
  }

  public actualizar(examenList: Examen[]): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'actualizar', examenList);
  }

  public consultarMisExamenes(): Observable<Examen[]> {
    return this.http.get<Examen[]>(this.dataUrl + 'consultarMisExamenes');
  }

  public consultarExamenesPendientesAsignados(): Observable<Examen[]> {
    return this.http.get<Examen[]>(this.dataUrl + 'misExamenesAsignados');
  }

  public consultarExamenesContestadosAsignados(): Observable<Examen[]> {
    return this.http.get<Examen[]>(this.dataUrl + 'misExamenesAsignados/contestados');
  }

  public consultarExamenesVencidosAsignados(): Observable<Examen[]> {
    return this.http.get<Examen[]>(this.dataUrl + 'misExamenesAsignados/vencidos');
  }

  public consultarMiExamen(idExamen: number): Observable<Examen> {
    return this.http.get<Examen>(this.dataUrl + `consultarMiExamen/${idExamen}`);
  }

  public consultarMiExamenIndividual(idExamen: number): Observable<Examen> {
    return this.http.get<Examen>(this.dataUrl + `consultarMiExamenIndividual/${idExamen}`);
  }

  public consultarCalificaciones(idProgramacionExamen: number): Observable<Examen[]> {
    return this.http.get<Examen[]>(this.dataUrl + `consultarCalificaciones/${idProgramacionExamen}`);
  }

  public descargarRespuestasPDF(idExamen: number) {
    return this.http.post(this.dataUrl + `descargarExamenPdf/${idExamen}`, {},
    {responseType: 'blob'}
    );
  }

  public consultarCantidadReactivos(idAsignatura : number , idNivelExamen : number): Observable<number> {
    return this.http.get<number>(this.dataUrl + `cantidadReactivos/${idAsignatura}/${idNivelExamen}`);
  }

}
