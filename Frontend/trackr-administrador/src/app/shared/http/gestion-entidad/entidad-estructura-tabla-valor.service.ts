import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EntidadTablaRegistroDto } from '@dtos/gestion-entidades/entidad-tabla-registro-dto';
import { RegistroTabla } from '@models/gestion-entidad/registro-tabla';
import { Observable } from 'rxjs';

@Injectable()
export class EntidadEstructuraTablaValorService {
  private dataUrl = 'entidadEstructuraTablaValor/';

  constructor(public http: HttpClient) {}

  consultarRegistroTablaPorTabulacion(idEntidadEstructura: number, idTabla: number) {
     return this.http.get<RegistroTabla[]>(this.dataUrl + `consultarRegistroTablaPorTabulacion/${idEntidadEstructura},${idTabla}`);
  }

  consultarPorPestanaSeccion(idEntidadEstructura: number, idTabla: number) {
    return this.http.get<RegistroTabla[]>(this.dataUrl + `pestanaSeccion/${idEntidadEstructura},${idTabla}`);
  }

  public agregar(registro: EntidadTablaRegistroDto): Observable<void> {
    return this.http.post<void>(this.dataUrl, registro);
  }

  public editar(registro: EntidadTablaRegistroDto): Observable<void> {
    return this.http.put<void>(this.dataUrl, registro);
  }

  public eliminar(registro: EntidadTablaRegistroDto): Observable<void> {
    return this.http.post<void>(this.dataUrl + 'eliminar', registro);
  }

}
