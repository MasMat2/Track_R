
import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { AlertController } from '@ionic/angular';
import { Router } from '@angular/router';
import { environment } from './../../environments/environment';
import { Storage } from '@ionic/storage';

import { Observable, throwError, from } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

const TOKEN_KEY = 'token-paciente';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(
    private router: Router,
    private storage: Storage,
    private alertController: AlertController
  ) {}

  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    let newRequest = null;

    return from(this.storage.get(TOKEN_KEY))
        .pipe(
          switchMap(token => {

            if (request.url.includes('i18n')) {
              newRequest = request.clone({
                url: request.url
              });
            } else if (request.url === 'login/authenticate') {
              newRequest = request.clone({
                url: environment.urlBackend + request.url
              });
            } else {
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
                // Estatus 409 son excepciones controladas por el backend.
                if (error.status === 409) {
                  this.presentAlert(error.error);
                } else if (error.status === 401 || error.status === 0) {
                  // Cuando el token enviado en la peticion es invalido, el servidor retorna un error 401
                  this.storage.remove("token-cliente");
                  this.router.navigate(['/login']);
                } else {
                  this.presentAlert(error.error);
                }
                  return throwError(() => new Error(error));
                })

              );
          })
        );
  }

  async presentAlert(reason: string) {

    var header = 'Error';
    if (reason === "Correo y/o contraseña incorrectos") {
      header = "Contraseña incorrecta";
    }
    const alert = await this.alertController.create({
      header:  header,
      message: reason,
      buttons: ['Aceptar']
    });
    
    await alert.present();
  }
}
