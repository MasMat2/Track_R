import { Component, OnInit } from '@angular/core';
import { NotificacionDoctorDTO } from '@dtos/notificaciones/notificacion-doctor-dto';
import { NotificacionDoctorHubService } from '@services/notificacion-doctor-hub.service';
import { GeneralConstant } from '@utils/general-constant';

@Component({
  selector: 'app-alertas',
  templateUrl: './alertas.component.html',
  styleUrls: ['./alertas.component.scss']
})
export class AlertasComponent implements OnInit {

  protected pacientesFueraDeRango: number;
  protected solicitudesDeChat: number;
  protected solicitudesDeVideo: number;
  private tipoNotificacion = GeneralConstant.TIPO_NOTIFICACION;

  constructor(
    private notificacionDoctorHubService: NotificacionDoctorHubService
  ) { }

  ngOnInit() {
    this.notificacionDoctorHubService.notificaciones$.subscribe((notificaciones) => {
      this.solicitudesDeChat = this.contar(notificaciones,this.tipoNotificacion.Chat );
      this.solicitudesDeVideo = this.contar(notificaciones, this.tipoNotificacion.Video);
      this.pacientesFueraDeRango = this.contar(notificaciones, this.tipoNotificacion.Alerta);
    });
  }

  private contar(notificaciones: NotificacionDoctorDTO[], claveNotificacion: string): number {
    return notificaciones
      .filter((notificacion) => notificacion.claveTipoNotificacion === claveNotificacion && notificacion.visto === false)
      .length;
  }

}
