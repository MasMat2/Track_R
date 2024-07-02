import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CustomAlertComponent } from '@sharedComponents/custom-alert/custom-alert.component';
import { CustomAlertData } from '@sharedComponents/interface/custom-alert-data';
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root',
})
export class AlertifyService {
  constructor(private dialog: MatDialog) {
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

  
  presentAlert(config: CustomAlertData, onClose: (result: string) => void) {
    const alert = this.dialog.open(CustomAlertComponent, {
      panelClass: 'custom-alert',
      data: config
    });

    alert.beforeClosed().subscribe(onClose);
  }


}
