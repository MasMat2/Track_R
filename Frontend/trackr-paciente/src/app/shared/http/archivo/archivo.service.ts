import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ArchivoFormDTO } from '../../Dtos/archivos/archivo-form-dto';
import { Observable } from 'rxjs';
import { ArchivoGetDTO } from '../../Dtos/archivos/archivo-get-dto';

@Injectable({
  providedIn: 'root'
})
export class ArchivoService {
  private dataUrl = 'ArchivoTrackr/';

  constructor(private http:HttpClient) { }

  public subirArchivo(archivoFormDTO:ArchivoFormDTO):Observable<void>{
    return this.http.post<void>(this.dataUrl,archivoFormDTO);
  }

  public getArchivo(idArchivo:number):Observable<ArchivoGetDTO>{
    return this.http.get<ArchivoGetDTO>(this.dataUrl+idArchivo)
  }
}
