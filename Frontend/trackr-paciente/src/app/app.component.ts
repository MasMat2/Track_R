import { Component, OnDestroy, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { NotificacionPacienteHubService } from '@services/notificacion-paciente-hub.service';
import { addIcons } from 'ionicons';
import { map, Observable, Subject, subscribeOn, takeUntil, tap } from 'rxjs';
import { NotificacionPacientePopOverDto } from './shared/Dtos/notificaciones/notificacion-paciente-popover-dto';
import { FechaService } from '@services/fecha.service';
import { NotificacionPacienteService } from '@http/gestion-perfil/notificacion-paciente.service';
import { take } from 'lodash';
import { routes } from './app.routes';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
  standalone: true,
  imports: [
    IonicModule
  ]
})
export class AppComponent implements OnDestroy, OnInit {
  constructor(private notificacionHubService : NotificacionPacienteHubService,
              private fechaService : FechaService,
              private notificacionPacienteService: NotificacionPacienteService,
              private router : Router) {
    this.notificacionPacienteService.iniciarConexionNoti$.pipe( takeUntil(this.destroy$)).subscribe(() => {
    this.consultarNotificaciones(); 
    })
                
    addIcons({
      'chevron-left': 'assets/img/svg/chevron-left.svg',
      'info': 'assets/img/svg/info.svg',
    })
  }

  private destroy$ = new Subject<void>();

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  async ngOnInit() {
  }


  private async consultarNotificaciones() { 
    //Iniciar conexión con el hub de forma global
    await this.notificacionHubService.iniciarConexion();
    this.notificacionHubService.notificaciones$.pipe(
      map((notificaciones) => {
        return notificaciones.map((notificacion) => {
          // Convertir la fecha UTC a la hora local
          const localDate = this.fechaService.fechaUTCAFechaLocal(notificacion.fechaAlta);
          const complemento = this.formatearComplemento(notificacion.complementoMensaje, notificacion.idTipoNotificacion);
          return {
            idTipoNotificacion: notificacion.idTipoNotificacion,
            id: notificacion.idNotificacionUsuario,
            titulo: notificacion.titulo,
            mensaje: notificacion.mensaje,
            complementoMensaje : complemento,
            complementoEsFecha: this.complementoEsFecha(notificacion.idTipoNotificacion),
            fecha: localDate, // Asignar la fecha local
            visto: notificacion.visto,
            idChat: notificacion.idChat,
            idUsuario: notificacion.idUsuario
          } as NotificacionPacientePopOverDto;
        });
      }),
     
    ).subscribe(async (notificacion) => {
      const ultimaNotificacion = notificacion[0];
       await this.notificacionPacienteService.validarMeet(ultimaNotificacion); 
    });
  }
  private formatearComplemento(complemento: string, tipoNotificacion: number){
    if(complemento == null){
      return null
    }

    if(this.esNotificacionExamen(tipoNotificacion)){
      const fecha = this.fechaService.fechaUTCAFechaLocal(complemento);
      return fecha;
    }
    else if(this.esNotificacionRecordatorio(tipoNotificacion)){
      const hora = this.fechaService.horaUTCAHoraLocal(complemento);
      return hora;
    }
    else{
      return complemento
    }

    
  }

  private esNotificacionExamen(idTipoNotificacion: number){
    return idTipoNotificacion === 4
  }

  private esNotificacionRecordatorio(idTipoNotificacion: number){
    return idTipoNotificacion === 6
  }

  
  private complementoEsFecha(idTipoNotificacion: number){
    return (this.esNotificacionExamen(idTipoNotificacion));
  }
}
