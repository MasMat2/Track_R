import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EntidadTablaRegistroDto } from '@dtos/gestion-entidades/entidad-tabla-registro-dto';
import { ExpedienteMuestrasGridDTO } from '@dtos/gestion-expediente/expediente-muestras-grid-dto';
import { ValoresPorClaveCampo } from '@dtos/gestion-expediente/valores-clave-campo';
import { ValoresFueraRangoGridDTO } from '@dtos/gestion-expediente/valores-fuera-rango-grid-dto';
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

  public consultarValoresFueraRango(idPadecimiento: number, idUsuario: number) {
    return this.http.get<ValoresFueraRangoGridDTO[]>(this.dataUrl + `valoresFueraRango/${idPadecimiento}/${idUsuario}`);
  }
  public consultarValoresTodasVariables(idPadecimiento: number, idUsuario: number) {
    return this.http.get<ValoresFueraRangoGridDTO[]>(this.dataUrl + `valoresTodasVariables/${idPadecimiento}/${idUsuario}`);
  }

  public consultarValoresPorClaveCampo(claveCampo: string, idUsuario: number, filtroTiempo: string): Observable<ValoresPorClaveCampo> {
    return this.http.get<ValoresPorClaveCampo>(this.dataUrl + `valoresPorClaveCampo/${claveCampo}/${idUsuario}/${filtroTiempo}`);
  }
  
  public consultarValoresFueraRangoUsuario(idUsuario : number) {
    return this.http.get<ValoresFueraRangoGridDTO[]>(this.dataUrl + `valoresFueraRangoGeneral/usuario/` + idUsuario);
  }

  public consultarGridMuestras(idUsuario : number) {
    return this.http.get<ExpedienteMuestrasGridDTO[]>(this.dataUrl + `grid/` + idUsuario);
  }
}
