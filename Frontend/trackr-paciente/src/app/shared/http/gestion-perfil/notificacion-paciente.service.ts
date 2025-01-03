import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, lastValueFrom, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AlertController } from '@ionic/angular/standalone';
import { UsuarioService } from '@services/usuario.service';
import { DataJitsiService } from '@pages/home/video-jitsi/service-jitsi/data-jitsi.service';
import { NotificacionPacientePopOverDto } from '../../Dtos/notificaciones/notificacion-paciente-popover-dto';
import { NotificacionPacienteHubService } from '@services/notificacion-paciente-hub.service';
import { NotificacionPacienteDTO } from '../../Dtos/notificaciones/notificacion-paciente-dto';
import { FechaService } from '@services/fecha.service';
import { NotificacionUsuarioService } from './notificacion-usuario.service';

@Injectable({
    providedIn: 'root'
})
export class NotificacionPacienteService {
    private dataUrl = 'notificacion/';
    private actualizarWidgetsSource = new BehaviorSubject<boolean>(false);
    actualizarWidgets$ = this.actualizarWidgetsSource.asObservable();

    private iniciarConexionNotiSource = new BehaviorSubject<boolean>(false);
    iniciarConexionNoti$ = this.iniciarConexionNotiSource.asObservable();
    private validandoMeet = false;


    constructor(public http: HttpClient, private route : Router,
        // private dataJitsiService: DataJitsiService,
        private usuarioService: UsuarioService,
        private alertController: AlertController,
        private notificacionHubService : NotificacionPacienteHubService,
        private fechaService : FechaService,
         private notificacionUsuarioService : NotificacionUsuarioService 
    ) { }

    public consultarPopOverPaciente(): Observable<NotificacionPacientePopOverDto[]> {
        return this.http.get<NotificacionPacientePopOverDto[]>(this.dataUrl + `usuario`);
    }

  /*   public marcarComoVista(idNotificacion : number) : Observable<void>{
        return this.http.post<void>(this.dataUrl + 'leida' , idNotificacion);
    } */

    actualizarWidgets() {
        this.actualizarWidgetsSource.next(true);
      }

  comenzarConexionNoti(){
    this.iniciarConexionNotiSource.next(true);
  }
      
  public async validarMeet(mensaje: NotificacionPacientePopOverDto) {
    if (!mensaje || this.validandoMeet) {
        return;  
    }

    if(mensaje.visto){
        return;
    }
    
    
    this.validandoMeet = true;
    if (mensaje.mensaje.includes('trackr-' + mensaje.idChat)) {
      const regex = /trackr-\d+-\d+/;
      const match = mensaje.mensaje.match(regex);
      if (match && match.length > 0) {
        const codigo = match[0];
        console.log("Codigo meet jitsi: " + codigo);
        await this.presentAlertVideollamada(mensaje, codigo);

      } else {
        console.error("Error al validar codigo meet jitsi.");
      }

      this.validandoMeet = false;
    }

    // if (mensaje.mensaje.includes('webrtc-' + mensaje.idChat)) {
    //   const regex = /webrtc-\d+-(\d+)/;
    //   const match = mensaje.mensaje.match(regex);
    //   if (match && match.length > 0) {
    //     const codigo = match[1];
    //     // this.route.navigate(['/home/chat', codigo]);

    //   } else {
    //     console.error("Error al validar codigo meet webRTC.");
    //   }

    //   this.validandoMeet = false;
    // }
  }

  
  protected async presentAlertVideollamada(notificacion : NotificacionPacientePopOverDto, codigo : string){
    const MENSAJE_TOMA = 'Tiene una llamada entrante'
    const noti = await lastValueFrom(this.notificacionUsuarioService.consultarNotificacion(notificacion.id));
    const idUsuario = noti.idUsuario as number;
      
    const imagenPerfil = await lastValueFrom(this.usuarioService.consultarImagenPerfil(idUsuario));
    
    const alert = await this.alertController.create({
      header: notificacion.titulo,
      subHeader: `${MENSAJE_TOMA}`,
      cssClass: 'custom-alert color-primary two-buttons',
      backdropDismiss: false, 
      message: '<img src="'+imagenPerfil+'" style="width: 100px; height: 100px; border-radius: 50%; margin: 0 auto; display: block;">',
      buttons: [
        {
          text: 'No contestar', 
          role: 'cancel',
          handler: () => {
            this.notificacionHubService.marcarComoVista(notificacion.id);
          }
        },
        {
          text: 'Contestar',
          role: 'confirm',
          handler: () => {
              this.contestarLlamada(codigo);
              this.notificacionHubService.marcarComoVista(notificacion.id);
          }
        }
      ],
    });

    await alert.present();
  }

  
  contestarLlamada(meetCode: string) {
    // this.route.navigate(['/home/video-jitsi/answer-call', meetCode]);
    // setTimeout(() => {
    //   this.dataJitsiService.contestarLlamada(meetCode);
    // }, 200);
  }


}