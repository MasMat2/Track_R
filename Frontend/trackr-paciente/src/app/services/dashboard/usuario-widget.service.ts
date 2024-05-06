import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { getAllWidgetTypes, WidgetType, isWidgetType, setAllWidgetTypes } from '@pages/home/dashboard/interfaces/widgets';
import { Observable, lastValueFrom, map, switchMap, tap } from 'rxjs';

@Injectable()
export class UsuarioWidgetService {
  private dataUrl = 'usuarioWidget/';

  constructor(
    private http: HttpClient
  ) { }
public consultarPorUsuarioEnSesion(): Observable<WidgetType[]> {
  return this.obtenerTiposDeWidgets().pipe(
    switchMap(clavesExistentes => {
      setAllWidgetTypes(clavesExistentes);
      return this.http.get<string[]>(this.dataUrl + 'usuario').pipe(
        tap(clavesUsuario => {
          for (const clave of clavesUsuario) {
            if (!isWidgetType(clave)) {
              console.error(`Clave de Widget Inválida: ${clave}`);
              //throw new Error(`Clave de Widget Inválida: ${clave}`);
            }
          }
        }),
        map(claves => claves as WidgetType[])
      );
    })
  );
}

  public obtenerTiposDeWidgets(): Observable<string[]> {
    return this.http.get<string[]>(this.dataUrl + 'widget-types');
  }

  public modificarPorUsuarioEnSesion(seleccionWidgets: string[]){
    return this.http.put(this.dataUrl + 'modificar', seleccionWidgets)
  }
}
