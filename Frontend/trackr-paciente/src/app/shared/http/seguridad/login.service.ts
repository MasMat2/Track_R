import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from '@models/seguridad/login-request';
import { LoginResponse } from '@models/seguridad/login-response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private dataUrl = 'login/';

  constructor(public http: HttpClient) { }

  public authenticate(loginRequest: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.dataUrl + 'authenticate/', loginRequest);
  }
}
