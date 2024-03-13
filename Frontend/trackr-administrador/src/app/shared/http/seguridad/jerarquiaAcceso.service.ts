import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JerarquiaAcceso } from '@models/seguridad/jerarquia-acceso';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JerarquiaAccesoService {

  private DATA_URL: string = "jerarquiaAcceso";

  constructor(private http: HttpClient) {}

  public consultar(idJerarquiaAcceso: number): Observable<JerarquiaAcceso> {
    return this.http.get<JerarquiaAcceso>(`${this.DATA_URL}/consultar/${idJerarquiaAcceso}`);
  }

  public consultarParaGrid(): Observable<JerarquiaAcceso[]> {
    return this.http.get<JerarquiaAcceso[]>(`${this.DATA_URL}/consultarParaGrid`);
  }

  public consultarParaSelector(): Observable<JerarquiaAcceso[]> {
    return this.http.get<JerarquiaAcceso[]>(`${this.DATA_URL}/consultarParaSelector`);
  }

  public agregar(jerarquiaAcceso: JerarquiaAcceso): Observable<void> {
    return this.http.post<void>(`${this.DATA_URL}/agregar`, jerarquiaAcceso);
  }

  public editar(jerarquiaAcceso: JerarquiaAcceso): Observable<void> {
    return this.http.post<void>(`${this.DATA_URL}/editar`, jerarquiaAcceso);
  }

  public eliminar(idJerarquiaAcceso: number): Observable<void> {
    return this.http.delete<void>(`${this.DATA_URL}/eliminar/${idJerarquiaAcceso}`);
  }

}
