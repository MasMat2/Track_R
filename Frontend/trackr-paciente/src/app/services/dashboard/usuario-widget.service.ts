import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, tap } from 'rxjs';
import { WidgetType, isWidgetType } from 'src/app/pages/paciente/dashboard/interfaces/widgets';

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
              throw new Error(`Clave de Widget Inválida: ${clave}`);
            }
          }
        }),
        map(claves => claves as WidgetType[])
      );
  }
}
