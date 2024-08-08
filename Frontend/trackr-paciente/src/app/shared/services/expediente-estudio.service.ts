import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ExpedienteEstudioGridDTO } from '../dtos/expediente-estudio-grid-dto';
import { ExpedienteEstudio } from '../dtos/expediente-estudio-dto';
import { ExpedienteEstudioFormularioCaptura } from '../dtos/expediente-estudio-formulario-captura-dto';
@Injectable({
  providedIn: 'root',
})
export class ExpedienteEstudioService {
  private dataUrl = 'expedienteEstudio/';
  private expedienteAddedSource = new BehaviorSubject<boolean>(false);
  public expedienteAdded$ = this.expedienteAddedSource.asObservable();

  constructor(public http: HttpClient) {}

  public consultarParaGrid(): Observable<ExpedienteEstudioGridDTO[]> {
    return this.http.get<ExpedienteEstudioGridDTO[]>(this.dataUrl + `grid`);
  }

  public agregarExpediente(
    expediente: ExpedienteEstudioFormularioCaptura
  ): Observable<void> {
    return this.http.post<void>(this.dataUrl, expediente);
  }
  public consultar(idExpedienteEstudio: number): Observable<ExpedienteEstudio> {
    return this.http.get<ExpedienteEstudio>(
      this.dataUrl + `consultar/${idExpedienteEstudio}`
    );
  }
  public eliminar(idExpedienteEstudio: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `${idExpedienteEstudio}`);
  }

  notifyExpedienteAdded() {
    this.expedienteAddedSource.next(true);
  }
}
