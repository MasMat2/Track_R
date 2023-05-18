import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import { Observable, catchError, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AdministradorAuthGuard } from './administrador-auth-guard.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(
    public auth: AdministradorAuthGuard,
    public modalService: MensajeService,
    private router: Router
  ) {}

  private readonly mensajeError = 'Ocurrió un error inesperado, favor de contactar al administrador del sistema.';

  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let token = localStorage.getItem(GeneralConstant.TOKEN_KEY);
    let newRequest = null;

    if (request.url === 'login/authenticate') {
      newRequest = request.clone({
        url: environment.urlBackend + request.url
      });
    }
    else {
      newRequest = request.clone({
        url: environment.urlBackend + request.url,
        setHeaders: {
          'Content-Type': 'application/json, text/plain',
          Authorization: `Bearer ${token}`
        }
      });
    }

    return next.handle(newRequest).pipe(
      catchError((error) => {
        // Excepciones controladas por el backend
        if (error.status === 409) {
          if (error.error instanceof Blob) {
            var reader = new FileReader();

            reader.onload = (params) => {
              if (reader.result){
                this.modalService.modalError(reader.result.toString());
              }
            }

            reader.readAsText(error.error);
          }
          else {
            this.modalService.modalError(error.error);
          }
        }
        // Mantenimiento
        else if (error.status === 503) {
          window.location.reload();
        }
        // Unauthorized
        else if (error.status === 401 || error.status === 0) {
          if (this.router.url.includes('administrador')) {
            localStorage.removeItem(GeneralConstant.TOKEN_KEY);
            this.router.navigate(['/login']);
          }
        }
        // Errores inesperados
        else if (error.status === 500) {
          this.modalService.modalError(this.mensajeError);
        }

        if (!environment.production) {
          console.error(error);
        }

        return throwError(() => error);
      })
    );
  }
}
