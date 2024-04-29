import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { WidgetType, isWidgetType } from '@pages/home/dashboard/interfaces/widgets';
import { Observable, map, tap } from 'rxjs';

@Injectable()
export class UsuarioWidgetService {
  private dataUrl = 'usuarioWidget/';

  constructor(
    private http: HttpClient
  ) { }

  public consultarPorUsuarioEnSesion(): Observable<WidgetType[]> {
    return this.http.get<string[]>(this.dataUrl + 'usuario')
      .pipe(
        tap(claves => {
          for (const clave of claves) {
            if (!isWidgetType(clave)) {
              console.error(`Clave de Widget Inválida: ${clave}`);
              //throw new Error(`Clave de Widget Inválida: ${clave}`);
            }
          }
        }),
        map(claves => claves as WidgetType[])
      );
  }

  public modificarPorUsuarioEnSesion(seleccionWidgets: string[]){
    return this.http.put(this.dataUrl + 'modificar', seleccionWidgets)
  }
}
