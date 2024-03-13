import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TipoWidget } from '@models/gestion-entidad/tipo-widget';
import { Observable } from 'rxjs';


@Injectable()
export class WidgetService {
  private dataUrl = 'widget/';

  constructor(
    private http: HttpClient
  ) { }

  public consultar(): Observable<TipoWidget[]> {
    return this.http.get<TipoWidget[]>(this.dataUrl + 'tipo')
  }
}
