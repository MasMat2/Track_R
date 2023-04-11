import { Injectable } from '@angular/core';
import { Router} from '@angular/router';
import { Observable } from 'rxjs';
import { PacienteAuthService } from './paciente-auth.service';

@Injectable({
  providedIn: 'root'
})
export class ClienteAuthGuard {

  constructor(
    private authService: PacienteAuthService,
    private router: Router
  ) {}

  canActivate(): Observable<boolean> | Promise<boolean> | boolean {

     if (!this.authService.isAuthenticated()) {
       this.router.navigate(['/login']);
       return false;
     }
    
    return this.authService.isAuthenticated()

  }
}
