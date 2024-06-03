import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AlertController } from '@ionic/angular';
import { Observable, from, lastValueFrom, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from './../../environments/environment';
import { AuthService } from './auth.service';
import { Constants } from '@utils/constants/constants';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(
    private alertController: AlertController,
    private authService: AuthService
  ) {}

  public intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return from(this.gestionarPeticion(request, next));
  }

 

  /**
   * Método auxiliar para gestionar la petición interceptada
   */
  private async gestionarPeticion(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Promise<HttpEvent<any>> {
    const peticion = await this.generarPeticion(request);

    return await lastValueFrom(
      next.handle(peticion).pipe(
        /* Manejo de errores */
        catchError((error) => {
          /* Excepciones controladas por el backend. */
          if (error.status === 409) {
            this.presentarAlerta('Ha sucedido un error', error.error);
          }
          /* Cuando el token enviado en la peticion es invalido, el servidor retorna un error 401 */
          else if (error.status === 401 || error.status === 0) {
            this.authService.cerrarSesion();
          }
          /* Errores inesperados */
          else {
            const MENSAJE_ERROR_INESPERADO: string = 'Ocurrió un error inesperado, favor de contactar al administrador del sistema.';
            this.presentarAlerta('Algo Salió Mal.', MENSAJE_ERROR_INESPERADO);
          }

          return throwError(() => new Error(error));
        })
      )
    );
  }

  /**
   * Generación de la petición http a enviar
   * @param request objeto de petición http inicial
   * @returns objeto de petición http final
   */
  private async generarPeticion(
    request: HttpRequest<any>
  ): Promise<HttpRequest<any>> {
    const token = await this.authService.obtenerToken();
    let newRequest: HttpRequest<any>;

    /* Petición anónima de autentificación */
    if (request.url === 'login/authenticate') {
      newRequest = request.clone({
        url: environment.urlBackend + request.url,
      });
    }
    /* Peticiones que requiren de un token */
    else {
      newRequest = request.clone({
        url: environment.urlBackend + request.url,
        setHeaders: {
          'Content-Type': 'application/json, text/plain',
          Authorization: `Bearer ${token}`,
        },
      });
    }

    return newRequest;
  }

  /**
   * Creación de modal de alerta, para la gestión de errores
   * @param mensaje string a mostrar en la alerta
   */
  private async presentarAlerta( header: string, subheader: string): Promise<void> {
    const alert = await this.alertController.create({
      header: header,
      subHeader: subheader,
      buttons: ['Cerrar'],
      cssClass: 'custom-alert color-error icon-info'
    });

    await alert.present();
  }
}
