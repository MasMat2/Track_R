import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { ExpedienteRecomendacionGridDTO } from '@dtos/gestion-expediente/expediente-recomendacion/expediente-recomendacion-grid-dto';
import { Observable } from "rxjs";
import { ExpedienteRecomendacionFormDTO } from "@dtos/gestion-expediente/expediente-recomendacion/expediente-recomendacion-form.dto";

@Injectable({
    providedIn : 'root'
})

export class ExpedienteRecomendacionService{
    private dataUrl = 'expedienteRecomendacion/';
    
    constructor(public http : HttpClient){}

    public consultarGridPorUsuario(idExpediente : number) : Observable<ExpedienteRecomendacionGridDTO[]>
    {
        return this.http.get<ExpedienteRecomendacionGridDTO[]>(this.dataUrl + `grid/usuario/${idExpediente}`);
    }
  
    public consultar(idRecomendacion: number): Observable<ExpedienteRecomendacionFormDTO> {
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
    

}