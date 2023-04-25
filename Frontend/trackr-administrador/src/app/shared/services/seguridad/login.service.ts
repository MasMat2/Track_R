import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from 'src/app/models/seguridad/login-request';
import { LoginResponse } from 'src/app/models/seguridad/login-response';

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
