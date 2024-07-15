import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { StorageService } from '@services/storage.service';
import { GeneralConstant } from '@utils/general-constant';
import { firstValueFrom, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private storage: StorageService,
    private router: Router,
    private http: HttpClient
  ) {}

  public async obtenerToken(): Promise<string | null> {
    return await this.storage.read(GeneralConstant.TOKEN_KEY);
  }

  public async guardarToken(token: string): Promise<void> {
    await this.storage.set(GeneralConstant.TOKEN_KEY, token);
  }

  public async cerrarSesion(reroute: boolean = true): Promise<void> {
    await this.storage.remove(GeneralConstant.TOKEN_KEY);

    if (reroute) {
      this.router.navigate(['/acceso/login']);
    }
  }

  public async isAuthenticated(): Promise<boolean> {
    const token = await this.obtenerToken();

    if(token == null || token == undefined){
      return false;
    }
    else{
      try {
        const valid = await firstValueFrom(this.validateToken());
        return valid ? true : false;
      }
      catch(error){
        return false;
      }
    }
  }

  private validateToken(): Observable<boolean>{
    return this.http.get<boolean>('login/validateToken');
  }
}
