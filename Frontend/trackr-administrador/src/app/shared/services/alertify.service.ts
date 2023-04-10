import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root',
})
export class AlertifyService {
  constructor() {
    alertify.set('notifier', 'position', 'top-right');
  }

  public mensajes: any = [];
  public mensajesExito: any = [];
  public maximoMensajes = 2;
  public maximoMensajesExito = 2;

  public confirm(message: string, okCallback: () => any): void {
    alertify.confirm(message, (e: any) => {
      if (e) {
        okCallback();
      }
    });
  }

  public success(message: string): void {
    this.mensajesExito.length < this.maximoMensajesExito
      ? this.mensajesExito.push(alertify.success(message))
      : this.mensajesExito.push(
          this.mensajesExito.shift().dismiss().setContent(message).push()
        );
  }

  public error(message: string): void {
    this.mensajes.length < this.maximoMensajes
      ? this.mensajes.push(alertify.error(message))
      : this.mensajes.push(
          this.mensajes.shift().dismiss().setContent(message).push()
        );
  }

  public warning(message: string): void {
    alertify.warning(message);
  }

  public message(message: string): void {
    alertify.message(message);
  }
}
