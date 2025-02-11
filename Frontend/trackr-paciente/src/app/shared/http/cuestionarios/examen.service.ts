import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Examen } from '@models/examen/examen';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ExamenService {
  private dataUrl = 'examen/';

  private actualizarCuestionariosSource = new BehaviorSubject<boolean>(false);
  actualizarCuestionarios$ = this.actualizarCuestionariosSource.asObservable();

  constructor(private http: HttpClient) {}

  public consultarMisExamenes(): Observable<Examen[]> {
    return this.http.get<Examen[]>(this.dataUrl + 'consultarMisExamenes');
  }

  public consultarMisExamenesContestados(): Observable<Examen[]> {
    return this.http.get<Examen[]>(this.dataUrl + 'MisExamenes/contestados');
  }

  public consultarMiExamen(idExamen: number): Observable<Examen> {
    return this.http.get<Examen>(this.dataUrl + `consultarMiExamen/${idExamen}`);
  }

  public consultarMiExamenIndividual(idExamen: number): Observable<Examen> {
    return this.http.get<Examen>(this.dataUrl + `consultarMiExamenIndividual/${idExamen}`);
  }

  public actualizarListadoExamenes() {
    this.actualizarCuestionariosSource.next(true);
  }

}
