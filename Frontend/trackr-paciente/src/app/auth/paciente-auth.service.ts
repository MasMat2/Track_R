import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { StorageService } from '../services/seguridad/storage.service';

import { GeneralConstant } from '@shared/general-constant';

@Injectable({
  providedIn: 'root'
})
@Injectable()
export class PacienteAuthService {
  constructor(
    private storage: StorageService,
    private router: Router
    ) {}

  public async obtenerToken(): Promise<string | null> {
    return await this.storage.read(GeneralConstant.TOKEN_KEY);
  }

  public async isAuthenticated() :Promise<boolean>{
    const token = await this.obtenerToken();

    if (token != null && token !== undefined) {
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  
  }
}