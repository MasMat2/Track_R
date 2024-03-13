import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProgramacionExamen } from '@models/examen/programacion-examen';

@Injectable({
  providedIn: 'root',
})
export class ProgramacionExamenService {
  private dataUrl = 'programacionExamen/';

  constructor(public http: HttpClient) {}

  consultarTodosParaSelector() {
    return this.http.get<ProgramacionExamen[]>(this.dataUrl + 'consultarTodosParaSelector');
  }

  consultar(idReactivo: number) {
    return this.http.get<ProgramacionExamen>(this.dataUrl + `consultar/${idReactivo}`);
  }

  consultarGeneral() {
    return this.http.get<ProgramacionExamen[]>(this.dataUrl + 'consultarGeneral');
  }

  agregar(programacionExamen: ProgramacionExamen) {
    return this.http.post<number>(this.dataUrl + 'agregar', programacionExamen);
  }

  editar(programacionExamen: ProgramacionExamen) {
    return this.http.put<void>(this.dataUrl + 'editar', programacionExamen);
  }

  eliminar(idProgramacionExamen: number) {
    return this.http.delete<void>(this.dataUrl + `eliminar/${idProgramacionExamen}`);
  }
}
