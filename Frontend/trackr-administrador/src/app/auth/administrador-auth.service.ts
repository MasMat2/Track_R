import { Injectable } from '@angular/core';

@Injectable()
export class AdministradorAuthService {
  constructor() {}

  public getToken(): string | null {
    return localStorage.getItem('token-administrador');
  }

  public isAuthenticated(): boolean {
    const token = this.getToken();
    return token != null && token !== undefined;
  }
}
