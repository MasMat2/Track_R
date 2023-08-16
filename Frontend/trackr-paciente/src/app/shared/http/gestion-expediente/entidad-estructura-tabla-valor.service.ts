import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ValoresFueraRangoGridDTO } from '@dtos/gestion-expediente/valores-fuera-rango-grid-dto';

@Injectable()
export class EntidadEstructuraTablaValorService {
  private dataUrl = 'entidadEstructuraTablaValor/';

  constructor(public http: HttpClient) {}

  // public agregar(registro: EntidadTablaRegistroDto): Observable<void> {
  //   return this.http.post<void>(this.dataUrl, registro);
  // }

  // public editar(registro: EntidadTablaRegistroDto): Observable<void> {
  //   return this.http.put<void>(this.dataUrl, registro);
  // }

  // public eliminar(registro: EntidadTablaRegistroDto): Observable<void> {
  //   return this.http.post<void>(this.dataUrl + 'eliminar', registro);
  // }

  public consultarValoresFueraRangoUsuarioSesion() {
    return this.http.get<ValoresFueraRangoGridDTO[]>(this.dataUrl + `valoresFueraRangoGeneral/usuarioSesion`);
  }

  // public consultarValoresTodasVariables(idPadecimiento: number, idUsuario: number) {
  //   return this.http.get<ValoresFueraRangoGridDTO[]>(this.dataUrl + `valoresTodasVariables/${idPadecimiento}/${idUsuario}`);
  // }
  
}
