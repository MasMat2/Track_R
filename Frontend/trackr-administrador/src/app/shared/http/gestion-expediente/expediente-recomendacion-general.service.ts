import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ExpedienteRecomendacionGridDTO } from '../../dtos/gestion-expediente/expediente-recomendacion/expediente-recomendacion-grid-dto';

@Injectable({
  providedIn: 'root'
})
export class ExpedienteRecomendacionGeneralService {
  private dataUrl = 'ExpedienteRecomendacionGeneral/';

  constructor(private http: HttpClient) { }

  public consultarGrid(): Observable<ExpedienteRecomendacionGridDTO[]>{
    return this.http.get<ExpedienteRecomendacionGridDTO[]>(this.dataUrl)

  }
}
