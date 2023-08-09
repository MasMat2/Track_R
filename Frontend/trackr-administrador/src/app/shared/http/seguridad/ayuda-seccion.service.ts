import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AyudaSeccion } from '@models/seguridad/ayuda-seccion';

@Injectable({
  providedIn: 'root'
})
export class AyudaSeccionService {
  private dataUrl = 'ayudaSeccion/';

  constructor(public http: HttpClient) {}

  public consultarTodosParaSelector() {
    return this.http.get<AyudaSeccion[]>(this.dataUrl + `consultarTodosParaSelector`);
  }

  public consultarParaGrid() {
    return this.http.get<AyudaSeccion[]>(this.dataUrl + 'consultarParaGrid');
  }

  public agregar(ayudaSeccion: AyudaSeccion): Observable<void>  {
    return this.http.post<void>(this.dataUrl + 'agregar', ayudaSeccion);
  }

  public editar(ayudaSeccion: AyudaSeccion): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', ayudaSeccion);
  }

  public eliminar(idAyudaSeccion: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idAyudaSeccion}`);
  }

}
