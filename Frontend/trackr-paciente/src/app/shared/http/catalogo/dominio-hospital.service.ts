import { Injectable } from '@angular/core';
import { DominioHospitalDto } from 'src/app/shared/Dtos/catalogo/dominio-hospital-dto';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DominioHospitalService {
  private dataUrl = 'DominioHospital/'

  constructor(private http:HttpClient) { }

  public obtenerDominioHospital(idDominio:number, idHospital:number):Observable<DominioHospitalDto>{
    return this.http.get<DominioHospitalDto>(`${this.dataUrl}${idHospital}/${idDominio}`)
  }

  public guardarDominioHospital(dominioHospital:DominioHospitalDto):Observable<void>{
    return this.http.post<void>(`${this.dataUrl}`,dominioHospital)
  }

  public editarDominioHospital(dominioHospital:DominioHospitalDto):Observable<void>{
    return this.http.put<void>(`${this.dataUrl}`,dominioHospital)
  }
}
