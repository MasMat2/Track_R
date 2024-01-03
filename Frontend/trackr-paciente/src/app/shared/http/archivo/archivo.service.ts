import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ArchivoDto } from '@dtos/archivos/archivo-dto';
import { ArchivoFormDTO } from '../../dtos/archivos/archivo-form-dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ArchivoService {
  private dataUrl = 'ArchivoTrackr/';

  constructor(private http:HttpClient) { }

  public subirArchivo(archivoFormDTO:ArchivoFormDTO):Observable<void>{
    return this.http.post<void>(this.dataUrl,archivoFormDTO);
  }
}
