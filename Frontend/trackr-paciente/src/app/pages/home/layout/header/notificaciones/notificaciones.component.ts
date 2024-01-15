import { CommonModule, NgClass, NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule} from '@ionic/angular';
import { NotificacionPacienteHubService } from '@services/notificacion-paciente-hub.service';
import { Observable , map} from 'rxjs';
import { NotificacionPacientePopOverDto } from '../../../../../shared/Dtos/notificaciones/notificacion-paciente-popover-dto';
import { NotificacionPacienteService } from '../../../../../shared/http/gestion-perfil/notificacion-paciente.service';

@Component({
  selector: 'app-notificaciones',
  templateUrl: './notificaciones.component.html',
  styleUrls: ['./notificaciones.component.scss'],
  standalone : true,
  imports : [
    IonicModule,
    NgFor,
    NgClass,
    CommonModule
  ]
})
export class NotificacionesComponent  implements OnInit 
{
  protected notificaciones$: Observable<NotificacionPacientePopOverDto[]>;
  
  constructor(private notificacionHubService : NotificacionPacienteHubService,
              private  notificacionPacienteService : NotificacionPacienteService){}

  ngOnInit() 
  {
    this.consultarNotificaciones();
  }

  private consultarNotificaciones(): void {
    this.notificaciones$ = this.notificacionHubService.notificaciones$.pipe(
      map((notificaciones) =>
        notificaciones.map((notificacion) => ({
          idTipoNotificacion: notificacion.idTipoNotificacion,
          id: notificacion.idNotificacionUsuario,
          titulo: notificacion.titulo,
          mensaje: notificacion.mensaje,
          fecha: notificacion.fechaAlta,
          visto: notificacion.visto,
        } as NotificacionPacientePopOverDto)) 
      )
    );
  }

  protected async marcarComoVista(idNotificacionUsuario: number  , visto : boolean , idTipoNotificacion : number) {
    if(!visto)
    {
      await this.notificacionHubService.marcarComoVista(idNotificacionUsuario);
      if(idTipoNotificacion == 6)
      {
        this.notificacionPacienteService.actualizarWidgets();
      }
      this.consultarNotificaciones();
    }
  }

  iconMappings: any = {
    1: { class: 'fa-solid fa-earth-americas', color: '#671e75' },
    2: { class: 'fa-solid fa-comment-dots', color: '#671e75' },
    4 : { class: 'fa-solid fa-user', color: '#671e75' },
    5: { class: 'fa-regular fa-circle-user', color: '#671e75' },
    6: { class: 'fa-solid fa-capsules', color: '#671e75' }
  };
}
