import { Component } from '@angular/core';
import { SafeUrl } from '@angular/platform-browser';
import { PanelNotificacionesComponent } from '@components/inicio/components/panel-notificaciones/panel-notificaciones.component';
import { NotificacionService } from '@http/notificaciones/notificacion.service';
import { NotificacionDoctorHubService } from '@services/notificacion-doctor-hub.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable, map } from 'rxjs';

@Component({
  selector: 'app-notificaciones',
  templateUrl: './notificaciones.component.html',
  styleUrls: ['./notificaciones.component.scss']
})
export class NotificacionesComponent {

  protected notificaciones$: Observable<{
    id: number,
    paciente: string,
    mensaje: string,
    fecha: Date,
    imagen?: string | SafeUrl,
    visto: boolean,
    idChat?: number
  }[]>;
  
  constructor( 
    private modalService: BsModalService,
    private bsModalRef: BsModalRef,
    private notificacionHubService: NotificacionDoctorHubService,
  ){

  }

  ngOnInit() {
    this.consultarNotificacionesNoVistas();
  }

  public mostrarNotificaciones(){
    this.bsModalRef = this.modalService.show(PanelNotificacionesComponent,{
      class: 'modal-notificaciones'
    })
    this.bsModalRef.content!.onClose = (cerrar: boolean) => {
      if (cerrar) {
      }
      this.bsModalRef.hide();
    };
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
          mensaje: notificacion.mensaje,
          fecha: notificacion.fechaAlta,
          visto: notificacion.visto,
        } as any))
      )
    );
  }

}
