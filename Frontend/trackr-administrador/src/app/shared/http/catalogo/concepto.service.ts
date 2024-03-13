
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Concepto } from '@models/catalogo/concepto';

@Injectable({
  providedIn: 'root'
})
export class ConceptoService {
  private dataUrl = 'concepto/';

  constructor(public http: HttpClient) { }

  public consultarTodosParaSelector() {
    return this.http.get<Concepto[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  public consultarSelectorParaPresentacion() {
    return this.http.get<Concepto[]>(this.dataUrl + 'consultarSelectorParaPresentacion');
  }

  public consultarTodosParaGrid() {
    return this.http.get<Concepto[]>(this.dataUrl + 'consultarTodosParaGrid');
  }

  public consultarParaDesgloseSelector() {
    return this.http.get<Concepto[]>(this.dataUrl + 'consultarParaDesgloseSelector');
  }

  public consultarOperativosParaSelector() {
    return this.http.get<Concepto[]>(this.dataUrl + 'consultarOperativosParaSelector');
  }

  public consultarPorTipo(claveTipo: string) {
    return this.http.get<Concepto[]>(this.dataUrl + `consultarPorTipo/${claveTipo}`);
  }

  public consultar(idConcepto: number): Observable<Concepto> {
    return this.http.get<Concepto>(this.dataUrl + `consultar/${idConcepto}`);
  }

  public agregar(concepto: Concepto): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'agregar', concepto);
  }

  public editar(concepto: Concepto): Observable<void> {
    return this.http.put<void>(this.dataUrl + 'editar', concepto);
  }

  public eliminar(idConcepto: number): Observable<void> {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idConcepto}`);
  }

}
