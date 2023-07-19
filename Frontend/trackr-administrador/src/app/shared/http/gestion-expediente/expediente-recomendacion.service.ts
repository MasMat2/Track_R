import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { ExpedienteRecomendacionGridDTO } from '@models/gestion-expediente/expediente-recomendacion';
import { Observable } from "rxjs";
import { ExpedienteRecomendacionFormDTO } from '../../dtos/gestion-expediente/expediente- recomendacion-form-dto';

@Injectable({
    providedIn : 'root'
})

export class ExpedienteRecomendacionService{
    private dataUrl = 'expedienteRecomendacion/';
    
    constructor(public http : HttpClient){}

    public consultarPorUsuario(idExpediente : number) : Observable<ExpedienteRecomendacionGridDTO[]>
    {
        return this.http.get<ExpedienteRecomendacionGridDTO[]>(this.dataUrl + `usuario/${idExpediente}`);
    }
  
    public consultarPorId(idRecomendacion: number): Observable<ExpedienteRecomendacionFormDTO> {
        return this.http.get<ExpedienteRecomendacionFormDTO>(this.dataUrl + `${idRecomendacion}`)
      }
      

     public agregar(expedienteRecomendacionDTO : ExpedienteRecomendacionFormDTO) : Observable<void>
     { 
        return this.http.post<void>(this.dataUrl, expedienteRecomendacionDTO);
     } 
     
    public editar(expedienteRecomendacionDTO : ExpedienteRecomendacionFormDTO) : Observable<void>
    {
        return this.http.put<void>(this.dataUrl , expedienteRecomendacionDTO);
    }

    public eliminar(idExpedienteRecomendacion : number) : Observable<void>
    {
        return this.http.delete<void>(this.dataUrl + `${idExpedienteRecomendacion}`);
    }
    
    public obtenerIdExpediente(idUsuario : number) : Observable<number>
    {
        return this.http.get<number>(this.dataUrl + `expediente/${idUsuario}`);
    }

}