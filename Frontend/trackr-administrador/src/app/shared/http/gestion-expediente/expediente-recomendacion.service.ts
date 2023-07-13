import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { ExpedienteRecomendacion } from '@models/gestion-expediente/expediente-recomendacion';
import { Observable } from "rxjs";
import { ExpedienteRecomendacionFormDTO } from '../../dtos/gestion-expediente/expediente- recomendacion-form-dto';

@Injectable(
    {providedIn : 'root'}
    )

export class ExpedienteRecomendacionService{
    private dataUrl = 'expedienteRecomendacion/';
    
    constructor(public http : HttpClient){}

    consultar(idExpediente : number)
    {
        return this.http.get<ExpedienteRecomendacion[]>(this.dataUrl + `consultar/${idExpediente}`);
    }

     agregar(expedienteRecomendacionDTO : ExpedienteRecomendacionFormDTO) : Observable<void>
     { 
        return this.http.post<void>(this.dataUrl, expedienteRecomendacionDTO);
     } 
     
    editar(idExpedienteRecomendacion : number , expedienteRecomendacionFormDTO : ExpedienteRecomendacionFormDTO) : Observable<void>
    {
   
        return this.http.put<void>(this.dataUrl + `${idExpedienteRecomendacion}` , expedienteRecomendacionFormDTO);
    }

    eliminar(idExpedienteRecomendacion : number) : Observable<void>
    {
        return this.http.delete<void>(this.dataUrl + `${idExpedienteRecomendacion}`);
    }




}