import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificacionService {
  private readonly endpoint = 'notificacion/';

  constructor(private http: HttpClient) { }

  public notificar(): Observable<void> {
    return this.http.post<void>(this.endpoint, null);
  }

}
