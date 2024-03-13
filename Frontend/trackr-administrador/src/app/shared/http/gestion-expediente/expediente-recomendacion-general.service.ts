import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ExpedienteRecomendacionGridDTO } from '../../dtos/gestion-expediente/expediente-recomendacion/expediente-recomendacion-grid-dto';
import { ExpedienteRecomendacionGeneralFormDTO } from '../../dtos/gestion-expediente/expediente-recomendacion-general/expediente-recomendacion-general-form-dto';

@Injectable({
  providedIn: 'root'
})
export class ExpedienteRecomendacionGeneralService {
  private dataUrl = 'ExpedienteRecomendacionGeneral/';

  constructor(private http: HttpClient) { }

  public consultarGrid(): Observable<ExpedienteRecomendacionGridDTO[]>{
    return this.http.get<ExpedienteRecomendacionGridDTO[]>(this.dataUrl)

  }

  public editarRecomendacionGeneral(expedienteRecomendacionGeneralFormDTO:ExpedienteRecomendacionGeneralFormDTO): Observable<void>{
    return this.http.put<void>(this.dataUrl,expedienteRecomendacionGeneralFormDTO);
  }

  public consultar(idExpedienteRecomendacionGeneral: number): Observable<ExpedienteRecomendacionGeneralFormDTO>{
    return this.http.get<ExpedienteRecomendacionGeneralFormDTO>(this.dataUrl+idExpedienteRecomendacionGeneral);
  }

  public eliminar(idExpedienteRecomendacionGeneral: number): Observable<void>{
    return this.http.delete<void>(this.dataUrl+idExpedienteRecomendacionGeneral);
  }

  public agregarTodos(expedienteRecomendacionGeneralFormDTO:ExpedienteRecomendacionGeneralFormDTO): Observable<void>{
    return this.http.post<void>(this.dataUrl,expedienteRecomendacionGeneralFormDTO);
  }

  public agregarPadecimiento(expedienteRecomendacionGeneralFormDTO:ExpedienteRecomendacionGeneralFormDTO):Observable<void>{
    return this.http.post<void>(this.dataUrl+'porPadecimiento/',expedienteRecomendacionGeneralFormDTO)
  }
  
  public agregarPaciente(expedienteRecomendacionGeneralFormDTO:ExpedienteRecomendacionGeneralFormDTO):Observable<void>{
    return this.http.post<void>(this.dataUrl+'porPaciente/',expedienteRecomendacionGeneralFormDTO);
  }
}
