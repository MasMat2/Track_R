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
    private cdr : ChangeDetectorRef
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
      map((notificaciones) =>{
          return notificaciones.map((notificacion) => ({
          idTipoNotificacion: notificacion.idTipoNotificacion,
          id: notificacion.idNotificacionUsuario,
          titulo: notificacion.titulo,
          mensaje: notificacion.mensaje,
          fecha: notificacion.fechaAlta,
          visto: notificacion.visto,
          idChat: notificacion.idChat
        } as NotificacionPacientePopOverDto)) }
      )
    );
  }

  // protected async marcarComoVista(notificacion : NotificacionPacientePopOverDto) {
  //   //http://localhost:8100/#/home/chat-movil/chat/340
  //   if(!notificacion.visto)
  //   {
  //     await this.notificacionHubService.marcarComoVista(notificacion.id);
  //     if(notificacion.idTipoNotificacion == GeneralConstant.ID_TIPO_NOTIFICACION_TOMA)
  //     {
  //       //await this.modalTomarToma(notificacion.mensaje);
  //       await this.presentAlertTomarTratamiento(notificacion)
  //     } 
  //   }
  //   this.consultarNotificaciones();
  //     if(notificacion.idChat !== null){
  //       this.router.navigate(['home','chat-movil','chat',notificacion.idChat])
  //       this.modalController.dismiss();
  //     }
  // }

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

  private esHoy(fecha: Date): boolean {
    const hoy = new Date();
    return fecha.toDateString() === hoy.toDateString();
  }

  private esEstaSemana(fecha: Date): boolean {
    const hoy = new Date();
    const haceUnaSemana = new Date();
    haceUnaSemana.setDate(hoy.getDate() - 7);
    return fecha >= haceUnaSemana && fecha < hoy;
  }

  private esAnteriorEstaSemana(fecha: Date): boolean {
    const hoy = new Date();
    const haceUnaSemana = new Date();
    haceUnaSemana.setDate(hoy.getDate() - 7);
    return fecha < haceUnaSemana;
  }

}
