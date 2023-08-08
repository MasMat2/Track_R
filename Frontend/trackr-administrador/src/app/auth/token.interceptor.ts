import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import { Observable, catchError, map, throwError } from 'rxjs';
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
      map((value: HttpEvent<any>) => {
        if (value instanceof HttpResponse) {
          const body = value.body;
          this.mapResponse(body);
        }

        return value;
      }),
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
        else if (error.status === 401) {
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

  private isIsoDateString(value: any): boolean {
    const ISO_DATE_FORMAT = /(\d{4}-[01]\d-[0-3]\dT[0-2]\d:[0-5]\d:[0-5]\d\.\d+)|(\d{4}-[01]\d-[0-3]\dT[0-2]\d:[0-5]\d:[0-5]\d)|(\d{4}-[01]\d-[0-3]\dT[0-2]\d:[0-5]\d)/;

    if (value === null || value === undefined) {
      return false;
    }

    if (typeof value === 'string'){
      return ISO_DATE_FORMAT.test(value);
    }

    return false;
  }

  private mapResponse(body: any){
    if (body === null || body === undefined ) {
      return body;
    }

    if (typeof body !== 'object' ){
      return body;
    }

    for (const key of Object.keys(body)) {
      const value = body[key];

      if (this.isIsoDateString(value)) {
        body[key] = new Date(value);
      }
      else if (typeof value === 'object') {
        this.mapResponse(value);
      }
    }
  }
}
