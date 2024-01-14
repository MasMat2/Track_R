import { Component, OnInit, Sanitizer } from '@angular/core';
import { NotificacionDoctorCapturaDTO } from '@dtos/notificaciones/notificacion-doctor-captura-dto';
import { NotificacionService } from '@http/notificaciones/notificacion.service';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { Usuario } from '@models/seguridad/usuario';
import { NotificacionDoctorHubService } from '@services/notificacion-doctor-hub.service';
import { Observable, map } from 'rxjs';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ModalPanelNotificacionesComponent } from './modal-panel-notificaciones/modal-panel-notificaciones.component';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-panel-notificaciones',
  templateUrl: './panel-notificaciones.component.html',
  styleUrls: ['./panel-notificaciones.component.scss']
})
export class PanelNotificacionesComponent implements OnInit {

  protected pacientes$: Observable<Usuario[]>;
  protected tiposNotificacion: { idTipoNotificacion: number, nombre: string }[] = [
    { idTipoNotificacion: 2, nombre: 'Chat' },
    { idTipoNotificacion: 3, nombre: 'Video' },
    { idTipoNotificacion: 4, nombre: 'Alerta' },
  ];

  protected idPaciente?: number;
  protected idTipoNotificacion?: number;
  protected mensaje: string;

  protected notificaciones$: Observable<{
    id: number,
    paciente: string,
    mensaje: string,
    fecha: Date,
    imagen?: string | SafeUrl,
    visto: boolean
  }[]>;

  constructor(
    private notificacionService: NotificacionService,
    private notificacionHubService: NotificacionDoctorHubService,
    private usuarioService: UsuarioService,
    private modalService: BsModalService,
    private sanitizer:DomSanitizer
  ) { }

  ngOnInit() {
    this.pacientes$ = this.usuarioService.consultarPorRol("014");
    this.consultarNotificaciones();
  }

  private consultarNotificaciones(): void {
    this.notificaciones$ = this.notificacionHubService.notificaciones$
      .pipe(
        map(notificaciones => notificaciones.map((notificacion) => {
          return {
            id: notificacion.idNotificacionUsuario,
            paciente: notificacion.nombrePaciente,
            mensaje: notificacion.mensaje,
            fecha: notificacion.fechaAlta,
            imagen: (notificacion.imagen !== null || notificacion.imagen !== undefined) ? this.sanitizer.bypassSecurityTrustUrl(notificacion.imagen || '') : undefined,
            visto: notificacion.visto
          };
        }))
      );
  }

  protected notificar(): void {
    var dto: NotificacionDoctorCapturaDTO = {
      idPaciente: this.idPaciente!,
      idTipoNotificacion: this.idTipoNotificacion!,
      mensaje: this.mensaje
    };

    this.notificacionService.notificar(dto).subscribe();
  }

  protected async marcarComoVista(idNotificacionUsuario: number) {
    await this.notificacionHubService.marcarComoVista(idNotificacionUsuario);
    this.consultarNotificaciones();
  }

  protected mostrarModal(notificacion:any){
    const initialState = {
      notificacion
    }
    this.modalService.show(ModalPanelNotificacionesComponent,{initialState});
  }

}
