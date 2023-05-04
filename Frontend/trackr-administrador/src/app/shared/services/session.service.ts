import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BnNgIdleService } from 'bn-ng-idle';
import jwt_decode from 'jwt-decode';
import { GeneralConstant } from '@utils/general-constant';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
  constructor(public http: HttpClient, private router: Router, private bnIdle: BnNgIdleService) {}

  public obtenerIdUsuarioSesion(): number | null {
    const token = localStorage.getItem(GeneralConstant.CLAVE_TOKEN_ADMINISTRADOR);
    const decodedToken = this.getDecodedAccessToken(token);
    return decodedToken ? +decodedToken.nameid : null;
  }

  public obtenerIdCompaniaUsuarioSesion(): number | null {
    const token = localStorage.getItem(GeneralConstant.CLAVE_TOKEN_ADMINISTRADOR);
    const decodedToken = this.getDecodedAccessToken(token);
    return decodedToken ? +decodedToken.ic : null;
  }

  getDecodedAccessToken(token: string | null): any {
    if (token === null) {
      return null;
    }

    try {
        return jwt_decode(token);
    }
    catch(Error){
        return null;
    }
  }
}
