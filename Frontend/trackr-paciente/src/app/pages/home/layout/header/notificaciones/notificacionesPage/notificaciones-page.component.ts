import { Component, OnInit } from '@angular/core';
import { IonicModule, PopoverController } from '@ionic/angular';
import { NotificacionesComponent } from '../notificaciones.component';
import { NotificacionPacienteHubService } from '@services/notificacion-paciente-hub.service';
import { Observable, map } from 'rxjs';
import { CommonModule } from '@angular/common';
import { NotificacionPacientePopOverDto } from 'src/app/shared/Dtos/notificaciones/notificacion-paciente-popover-dto';

@Component({
  selector: 'app-notificaciones-page',
  templateUrl: './notificaciones-page.component.html',
  styleUrls: ['./notificaciones-page.component.scss'],
  standalone : true,
  imports : [
    IonicModule,
    CommonModule
  ]
})
export class NotificacionesPageComponent implements OnInit
{
  protected notificaciones$: Observable<NotificacionPacientePopOverDto[]>; 

  constructor(private popoverControler : PopoverController,
              private notificacionHubService : NotificacionPacienteHubService
              ){}

  ngOnInit() 
  {
    this.consultarNotificacionesNoVistas();
  }

  async _openPopover(ev : any)
  {
    const popover = await this.popoverControler.create({
      component : NotificacionesComponent,
      event : ev
    })
    return await popover.present();
  }

  private consultarNotificacionesNoVistas(): void {
    this.notificaciones$ = this.notificacionHubService.notificaciones$.pipe(
      map((notificaciones) =>
        notificaciones
        .filter((n) => n.visto == false)
        .map((notificacion) => 
        ({
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

}
