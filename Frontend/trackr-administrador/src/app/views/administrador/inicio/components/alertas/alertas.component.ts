import { Component, OnInit } from '@angular/core';
import { NotificacionDoctorDTO } from '@dtos/notificaciones/notificacion-doctor-dto';
import { NotificacionDoctorHubService } from '@services/notificacion-doctor-hub.service';

@Component({
  selector: 'app-alertas',
  templateUrl: './alertas.component.html',
  styleUrls: ['./alertas.component.scss']
})
export class AlertasComponent implements OnInit {

  protected pacientesFueraDeRango: number;
  protected solicitudesDeChat: number;
  protected solicitudesDeVideo: number;

  constructor(
    private notificacionDoctorHubService: NotificacionDoctorHubService
  ) { }

  ngOnInit() {
    this.notificacionDoctorHubService.notificaciones$.subscribe((notificaciones) => {
      this.solicitudesDeChat = this.contar(notificaciones, 2);
      this.solicitudesDeVideo = this.contar(notificaciones, 3);
      this.pacientesFueraDeRango = this.contar(notificaciones, 4);
    });
  }

  private contar(notificaciones: NotificacionDoctorDTO[], idTipoNotificacion: number): number {
    return notificaciones
      .filter((notificacion) => notificacion.idTipoNotificacion === idTipoNotificacion && notificacion.visto === false)
      .length;
  }

}
