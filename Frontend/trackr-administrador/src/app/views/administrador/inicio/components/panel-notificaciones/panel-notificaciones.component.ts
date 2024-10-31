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
import { Router } from '@angular/router';
import { Notificacion } from '@dtos/notificaciones/notificacion-dto';
import { MeetService } from '@http/chats/meet.service';
import { FechaService } from '@services/fecha.service';
import { EncryptionService } from '@services/encryption.service';

@Component({
  selector: 'app-panel-notificaciones',
  templateUrl: './panel-notificaciones.component.html',
  styleUrls: ['./panel-notificaciones.component.scss']
})
export class PanelNotificacionesComponent implements OnInit {

  protected iconMappings: any = {
    2: { name: 'message-square-more'},
    3: {name : 'phone'},
    4: { name: 'chevron-right' },

  };

  protected pacientes$: Observable<Usuario[]>;
  // protected tiposNotificacion: { idTipoNotificacion: number, nombre: string }[] = [
  //   { idTipoNotificacion: 2, nombre: 'Chat' },
  //   { idTipoNotificacion: 3, nombre: 'Video' },
  //   { idTipoNotificacion: 4, nombre: 'Alerta' },
  // ];

  protected idPaciente?: number;
  protected idTipoNotificacion?: number;
  protected mensaje: string;

  protected notificaciones$: Observable<Notificacion[]>;
  protected clicDeshabilitado = false;

  constructor(
    private notificacionService: NotificacionService,
    private notificacionHubService: NotificacionDoctorHubService,
    private usuarioService: UsuarioService,
    private modalService: BsModalService,
    private sanitizer:DomSanitizer,
    private router:Router,
    private meetService: MeetService,
    private fechaService: FechaService,
    private encryptionService: EncryptionService
  ) { }

  ngOnInit() {
    this.pacientes$ = this.usuarioService.consultarPorRol("014");
    this.consultarNotificaciones();
  }

  private redirigirExpediente(idPaciente: string, idPadecimiento : number): void {
    const queryParams = this.encryptionService.generateURL({
      i: idPaciente.toString(),
      p: idPadecimiento.toString()
    });
  
    this.router.navigate(
      ['/administrador/gestion-paciente/paciente/expediente-formulario'],
      {
        queryParams: queryParams,
      }
    );
  }

  private consultarNotificaciones(): void {
    this.notificaciones$ = this.notificacionHubService.notificaciones$
      .pipe(
        map(notificaciones => notificaciones.map((notificacion) => {
          return {
            id: notificacion.idNotificacionUsuario,
            idTipoNotificacion: notificacion.idTipoNotificacion,
            paciente: notificacion.nombrePaciente,
            mensaje: notificacion.mensaje,
            fecha: this.fechaService.fechaUTCAFechaLocal(notificacion.fechaAlta),
            imagen: (notificacion.imagen !== null || notificacion.imagen !== undefined) ? this.sanitizer.bypassSecurityTrustUrl(notificacion.imagen || '') : undefined,
            visto: notificacion.visto,
            idChat: notificacion.idChat,
            idPadecimiento: notificacion.idPadecimiento,
            idPaciente: notificacion.idPaciente
          } as Notificacion;
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

  protected async marcarComoVista(notificacion: Notificacion) {

    if(notificacion.idPaciente && notificacion.idPadecimiento){
      this.redirigirExpediente(notificacion.idPaciente , notificacion.idPadecimiento as number);
    }
     this.clicDeshabilitado = true;
    try {
      if(! notificacion.visto){
        await this.notificacionHubService.marcarComoVista(notificacion.id);
      }
      await this.meetService.redirigirMeet(notificacion);
      this.consultarNotificaciones();
    } catch (error) {
      console.error('Error al marcar como vista:', error);
    } finally {
      this.clicDeshabilitado = false;
    } 
  }

  protected mostrarModal(notificacion:any){
    if(notificacion.idChat !== null){
      this.router.navigate(['administrador','chat', notificacion.idChat])
      this.modalService.hide();
    }
    else {
      const initialState = {
        notificacion
      }
      this.modalService.show(ModalPanelNotificacionesComponent,{initialState});
    }
  }

  protected cerrarModal(){
    this.modalService.hide();
  }

}
