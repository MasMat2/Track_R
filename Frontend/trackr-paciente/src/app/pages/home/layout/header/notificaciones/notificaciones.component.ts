import { NgClass, NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { NotificacionPacienteService } from '../../../../../shared/http/gestion-perfil/notificacion-paciente.service';
import { NotificacionPacientePopOverDto } from 'src/app/shared/Dtos/perfil/notificacion-paciente-dto';


@Component({
  selector: 'app-notificaciones',
  templateUrl: './notificaciones.component.html',
  styleUrls: ['./notificaciones.component.scss'],
  standalone : true,
  imports : [
    IonicModule,
    NgFor,
    NgClass
  ]
})
export class NotificacionesComponent  implements OnInit 
{
  protected notificaciones: NotificacionPacientePopOverDto[];

  constructor(
    private notificacionPacienteService : NotificacionPacienteService
  ){}

  ngOnInit() 
  {
    this.notificacionPacienteService.consultarPopOverPaciente().subscribe((data) => {
      this.notificaciones = data;
    })
  }
  
  private consultarNotificaciones() : void
  {
   
  }

  iconMappings: any = {
    1: { class: 'fa-solid fa-earth-americas', color: '#671e75' },
    5: { class: 'fa-regular fa-circle-user', color: '#671e75' },
    6: { class: 'fa-solid fa-capsules', color: '#671e75' }
  };

  imprimirNotificaciones() {
    console.log(this.notificaciones);
  }

  async imprimirIdNotificacion(idNotificacion: number) {
    console.log(idNotificacion);
    /* try {
      await this.notificacionHubService.marcaVista(idNotificacion);
      this.consultarNotificaciones();
      console.log(`Notificación con ID ${idNotificacion} marcada como vista`);
    } catch (error) {
      console.error('Error al marcar notificación como vista', error);
    } */
  }
  



  

}
