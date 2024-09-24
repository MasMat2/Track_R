import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Reactivo } from '@models/examen/reactivo';
import { Observable } from 'rxjs';
import { ArchivoDto } from '@dtos/archivos/archivo-dto';

@Injectable({
  providedIn: 'root'
})
export class ReactivoService {
  private dataUrl = 'reactivo/';

  constructor(private http: HttpClient) {}

  public consultarTodosParaSelector(): Observable<Reactivo[]> {
    return this.http.get<Reactivo[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  public consultar(idReactivo: number): Observable<Reactivo> {
    return this.http.get<Reactivo>(this.dataUrl + `consultar/${idReactivo}`);
  }

  public consultarGeneral(): Observable<Reactivo[]> {
    return this.http.get<Reactivo[]>(this.dataUrl + 'consultarGeneral');
  }

  public agregar(reactivo: Reactivo): Observable<number> {
    return this.http.post<number>(this.dataUrl + 'agregar', reactivo);
  }

  public editar(reactivo: Reactivo): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', reactivo);
  }

  public eliminar(idReactivo: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idReactivo}`);
  }

  consultarArchivo(idReactivo:number): Observable<ArchivoDto> {
    return this.http.get<ArchivoDto>(this.dataUrl + `consultarArchivo/${idReactivo}`);
  }
}
