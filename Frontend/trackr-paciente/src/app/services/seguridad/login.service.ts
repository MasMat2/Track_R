import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from 'src/app/models/seguridad/login-request';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private dataUrl = 'login/';

  constructor(public http: HttpClient) { }

  public authenticate(loginRequest: LoginRequest): Observable<any> {
    return this.http.post<any>(this.dataUrl + 'authenticate/', loginRequest);
  }
}
