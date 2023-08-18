import { Component, OnInit } from '@angular/core';
import { NotificacionService } from '../../../../../shared/http/notificaciones/notificacion.service';
import { NotificacionHubService } from '@services/notificacion-hub.service';
import { Observable, map } from 'rxjs';

@Component({
  selector: 'app-panel-notificaciones',
  templateUrl: './panel-notificaciones.component.html',
  styleUrls: ['./panel-notificaciones.component.scss']
})
export class PanelNotificacionesComponent implements OnInit {

  protected notificaciones$: Observable<{
    id: number,
    paciente: string,
    mensaje: string,
    fecha: Date,
    imagen?: string,
    visto: boolean
  }[]>;

  constructor(
    private notificacionService: NotificacionService,
    private notificacionHubService: NotificacionHubService
  ) { }

  ngOnInit() {
    this.notificaciones$ = this.notificacionHubService.notificacion$
      .pipe(
        map(notificaciones => notificaciones.map((notificacion) => {
          return {
            id: notificacion.idNotificacionUsuario,
            paciente: notificacion.origen,
            mensaje: notificacion.descripcion,
            fecha: notificacion.fechaAlta,
            imagen: undefined,
            visto: notificacion.visto
          };
        }))
      );
  }

  protected notificar(): void {
    this.notificacionService.notificar().subscribe();
  }

  protected marcarComoVista(idNotificacionUsuario: number) {
    this.notificacionHubService.marcarComoVista(idNotificacionUsuario);
  }

}
