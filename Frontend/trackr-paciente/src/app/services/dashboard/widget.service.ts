import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap, throwError } from 'rxjs';
import { Widget } from 'src/app/models/dashboard/widget';
import { isWidgetType } from 'src/app/pages/paciente/dashboard/interfaces/widgets';

@Injectable()
export class WidgetService {
  private dataUrl = 'widget/';

  constructor(
    private http: HttpClient
  ) { }

  public consultar(): Observable<Widget[]> {
    return this.http.get<Widget[]>(this.dataUrl)
      .pipe(
        tap(widgets => {
          for (const widget of widgets) {
            if (!isWidgetType(widget.clave)) {
              throwError(() => new Error(`La clave de Widget no ha sido configurada: ${widget.clave}`));
            }
          }
        })
      );
  }
}
