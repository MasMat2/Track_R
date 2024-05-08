import { Component, OnInit } from '@angular/core';
import { IonicModule, PopoverController } from '@ionic/angular';
import { NotificacionesComponent } from '../notificaciones.component';
import { NotificacionPacienteHubService } from '@services/notificacion-paciente-hub.service';
import { Observable, map } from 'rxjs';
import { CommonModule, NgIf } from '@angular/common';
import { NotificacionPacientePopOverDto } from 'src/app/shared/Dtos/notificaciones/notificacion-paciente-popover-dto';
import { addIcons } from 'ionicons';
import { notificationsOutline, ellipse } from 'ionicons/icons'
import { ModalController } from '@ionic/angular/standalone';

@Component({
  selector: 'app-notificaciones-page',
  templateUrl: './notificaciones-page.component.html',
  styleUrls: ['./notificaciones-page.component.scss'],
  standalone : true,
  imports : [
    IonicModule,
    CommonModule,
    NgIf
  ]
})
export class NotificacionesPageComponent implements OnInit
{
  protected notificaciones$: Observable<NotificacionPacientePopOverDto[]>;

  constructor(
    private modalController : ModalController,
    private notificacionHubService : NotificacionPacienteHubService
  ){
    addIcons({notificationsOutline, ellipse})
  }

  ngOnInit()
  {
    this.consultarNotificacionesNoVistas();
  }

  protected async openModal(){
    const modal = await this.modalController.create({
      component: NotificacionesComponent,
    });

    await modal.present();
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
