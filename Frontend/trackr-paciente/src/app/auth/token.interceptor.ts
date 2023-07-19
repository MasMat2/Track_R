import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AlertController } from '@ionic/angular';
import { StorageService } from '@services/storage.service';
import { GeneralConstant } from '@utils/general-constant';
import { Observable, from, lastValueFrom, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from './../../environments/environment';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  private token = "";
  private mensajeErrorInesperado = 'Ocurrió un error inesperado, favor de contactar al administrador del sistema.';

  constructor(
    private router: Router,
    private alertController: AlertController,
    private storage: StorageService
  )
  {}

  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return from(this.gestionarPeticion(request, next));
  }

  /**
   * Método auxiliar para gestionar la petición interceptada
   */
  private async gestionarPeticion(request: HttpRequest<any>, next: HttpHandler): Promise<HttpEvent<any>> {

    await this.obtenerToken();
    const peticion = await this.generarPeticion(request);

    return await lastValueFrom(
      next.handle(peticion).pipe(
      /* Manejo de errores */
        catchError((error) => {
      /* Excepciones controladas por el backend. */
          if (error.status === 409)
          {
            this.presentarAlerta(error.error);
          }
      /* Cuando el token enviado en la peticion es invalido, el servidor retorna un error 401 */
          else if (error.status === 401 || error.status === 0)
          {
            this.storage.remove("token-cliente");
            this.router.navigate(['/login']);
          }
      /* Errores inesperados */
          else
          {
            this.presentarAlerta(this.mensajeErrorInesperado);
          }
            return throwError(() => new Error(error));
      }))
    );

  }

  /**
   * Generación de la petición http a enviar
   * @param request objeto de petición http inicial
   * @returns objeto de petición http final
   */
  private async generarPeticion(request: HttpRequest<any>): Promise<HttpRequest<any>> {
    let peticion = null;

    /* Petición anónima de autentificación */
    if (request.url === 'login/authenticate')
    {
      peticion = request.clone({
        url: environment.urlBackend + request.url
      });
    }
    /* Peticiones que requiren de un token */
    else
    {
      peticion = request.clone({
        url: environment.urlBackend + request.url,
        setHeaders: {
          'Content-Type': 'application/json, text/plain',
          Authorization: `Bearer ${this.token}`
        }
      });
    }

    return peticion;
  }

  /**
   * Obtener el token paciente almacenado en el storage
   */
  private async obtenerToken(): Promise<void> {
    await this.storage.read(GeneralConstant.TOKEN_KEY).then((token) => {
      if (token) {
        this.token = token;
      }
    })
  }

  /**
   * Creación de modal de alerta, para la gestión de errores
   * @param mensaje string a mostrar en la alerta
   */
  private async presentarAlerta(mensaje: string): Promise<void> {
    var header = 'Error';
    if (mensaje === "Correo y/o contraseña incorrectos") {
      header = "Contraseña incorrecta";
    }

    const alert = await this.alertController.create({
      header:  header,
      message: mensaje,
      buttons: ['Aceptar']
    });

    await alert.present();
  }
}
