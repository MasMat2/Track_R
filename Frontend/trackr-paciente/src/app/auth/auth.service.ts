import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { StorageService } from '@services/storage.service';
import { GeneralConstant } from '@utils/general-constant';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private storage: StorageService,
    private router: Router
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

    // TODO: 2023-07-18 -> Validate expiration date
    return token !== null && token !== undefined;
  }
}
