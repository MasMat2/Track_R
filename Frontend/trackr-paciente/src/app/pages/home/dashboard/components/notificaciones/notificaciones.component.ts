import { CommonModule, NgClass, NgFor } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AlertController, IonicModule, PopoverController } from '@ionic/angular';
import { NotificacionPacienteHubService } from '@services/notificacion-paciente-hub.service';
import { Observable , map} from 'rxjs';
import { NotificacionPacientePopOverDto } from '../../../../../shared/Dtos/notificaciones/notificacion-paciente-popover-dto';
import { NotificacionPacienteService } from '../../../../../shared/http/gestion-perfil/notificacion-paciente.service';
import { GeneralConstant } from '@utils/general-constant';
import { Router } from '@angular/router';
import { addIcons } from 'ionicons';
import { ModalController } from '@ionic/angular/standalone';
import { Constants } from '@utils/constants/constants';
import { ExamenService } from '@http/cuestionarios/examen.service';
import { FechaService } from '../../../../../shared/services/fecha.service';

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

  //TODO: Extraer de la bd usando las claves.
  //iconos correspondientes de lucidIcons
  protected iconMappings: any = {
    1: { name: 'earth'},
    2: { name: 'message-square-dot'},
    4: { name: 'user' },
    5: { name: 'circle-user'},
    6: { name: 'pill'}
  };

  constructor(
    private notificacionHubService : NotificacionPacienteHubService,
    private  notificacionPacienteService : NotificacionPacienteService,
    private alertController : AlertController,
    private router:Router,
    private popOverController:PopoverController,
    private modalController: ModalController,
    private cdr : ChangeDetectorRef,
    private examenService : ExamenService,
    private fechaService: FechaService,
  ){ addIcons({
    'close' : 'assets/img/svg/x.svg',
    'circle-user' : 'assets/img/svg/circle-user.svg',
    'user' : 'assets/img/svg/user.svg',
    'earth' : 'assets/img/svg/earth.svg',
    'message-square-dot' : 'assets/img/svg/message-square-dot.svg',
    'pill' : 'assets/img/svg/pill.svg',
    })
  }

  ngOnInit() 
  {
    this.consultarNotificaciones();
  }

  private consultarNotificaciones(): void {
    this.notificaciones$ = this.notificacionHubService.notificaciones$.pipe(
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
            idChat: notificacion.idChat
          } as NotificacionPacientePopOverDto;
        });
      })
    );
  }

  protected async marcarComoVista(event: Event, notificacion: NotificacionPacientePopOverDto) {
    event.preventDefault();
    event.stopPropagation();
  
    const scrollPosition = document.documentElement.scrollTop || document.body.scrollTop;
  
    this.cdr.detach(); 
  
    const navigateAndDismiss = async (path: any[]) => {
      await this.modalController.dismiss();
      this.router.navigate(path);
    };

    if (notificacion.idTipoNotificacion == GeneralConstant.ID_TIPO_NOTIFICACION_TOMA && !notificacion.visto) {
      await this.presentAlertTomarTratamiento(notificacion);
    } else if (notificacion.idChat !== null) {
      if (!notificacion.visto) 
        await this.notificacionHubService.marcarComoVista(notificacion.id);
      
      await navigateAndDismiss(['home', 'chat-movil', 'chat', notificacion.idChat]);
    } else if (notificacion.idTipoNotificacion == GeneralConstant.ID_TIPO_NOTIFICACION_ALERTA) {
      if (!notificacion.visto) 
        await this.notificacionHubService.marcarComoVista(notificacion.id);
        this.examenService.actualizarListadoExamenes();
      await navigateAndDismiss(['home', 'cuestionarios', 'misCuestionarios']);
    }else{
      if (!notificacion.visto) 
        await this.notificacionHubService.marcarComoVista(notificacion.id);
    }
    this.consultarNotificaciones();
  
    setTimeout(() => {
      window.scrollTo(0, scrollPosition); // Restore scroll position
      this.cdr.reattach(); // Reattach change detection
    }, 0);
  }

  protected async presentAlertTomarTratamiento(notificacion : NotificacionPacientePopOverDto){
    const MENSAJE_TOMA = '¿Tomó la dosis a tiempo?'
    const alert = await this.alertController.create({
      header: notificacion.titulo,
      subHeader: `${notificacion.mensaje} \n ${MENSAJE_TOMA}`,
      cssClass: 'custom-alert color-primary icon-pill two-buttons',
      backdropDismiss: false,
      buttons: [
        {
          text: 'No tomé la dosis', 
          role: 'cancel',
          handler: () => {
            this.notificacionHubService.marcarComoVista(notificacion.id, false);
            this.notificacionPacienteService.actualizarWidgets();
          }
        },
        {
          text: 'Sí tomé la dosis',
          role: 'confirm',
          handler: () => {
            this.notificacionHubService.marcarComoVista(notificacion.id, true);
            this.notificacionPacienteService.actualizarWidgets();
          }
        }
      ],
    });

    await alert.present();
  }

  protected async presentAlertSuccess() {

    const alertSuccess = await this.alertController.create({
      header: 'Tratamiento registrado',
      subHeader: 'Se ha registrado correctamente la toma del tratamiento.',
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm',
      }],
      cssClass: 'custom-alert-success',
    });

    await alertSuccess.present();
  }

  protected cerrarModal(){
    this.modalController.dismiss();
  }

  protected filtrarNotificacionesHoy(notificaciones: NotificacionPacientePopOverDto[] | null): NotificacionPacientePopOverDto[] {
    if(notificaciones)
      return notificaciones.filter(n => this.esHoy(n.fecha));
    else
      return [];
  }

  protected filtrarNotificacionesSemana(notificaciones: NotificacionPacientePopOverDto[] | null): NotificacionPacientePopOverDto[] {
    if(notificaciones)
      return notificaciones.filter(n => this.esEstaSemana(n.fecha));
    else
      return [];
  }

  protected filtrarNotificacionesAnterioresSemana(notificaciones: NotificacionPacientePopOverDto[] | null): NotificacionPacientePopOverDto[] {
    if(notificaciones)
      return notificaciones.filter(n => this.esAnteriorEstaSemana(n.fecha));
    else
      return [];
  }

  private esHoy(fecha: string): boolean {
    const hoy = new Date();
    const date = new Date(fecha);

    return date.toDateString() === hoy.toDateString();
  }

  private esEstaSemana(fecha: string): boolean {
    const hoy = new Date();
    const date = new Date(fecha);
    const haceUnaSemana = new Date();

    haceUnaSemana.setDate(hoy.getDate() - 7);
    return date >= haceUnaSemana && date < hoy;
  }

  private esAnteriorEstaSemana(fecha: string): boolean {
    const hoy = new Date();
    const date = new Date(fecha);

    const haceUnaSemana = new Date();
    haceUnaSemana.setDate(hoy.getDate() - 7);
    return date < haceUnaSemana;
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

}
