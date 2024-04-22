import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { IonicModule, ModalController } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { chevronForward, chevronDown} from 'ionicons/icons';
import { AuthService } from '../../../../auth/auth.service';
import { AlertController } from '@ionic/angular/standalone';
import { Constants } from '@utils/constants/constants';
import { Observable } from 'rxjs';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { UsuarioExpedienteGridDTO } from 'src/app/shared/Dtos/seguridad/usuario-expediente-grid-dto copy';
import { InformacionPerfilDto } from 'src/app/shared/Dtos/perfil/informacion-perfil-dto';
import { TerminosYCondicionesComponent } from '@sharedComponents/terminos-y-condiciones/terminos-y-condiciones.component';
import { InfoLibreriasOpenSourceComponent } from '@sharedComponents/info-librerias-opensource/info-librerias-opensource.component';


@Component({
  selector: 'app-inicio-perfil',
  templateUrl: './inicio-perfil.component.html',
  styleUrls: ['./inicio-perfil.component.scss'],
  standalone: true,
  imports : [
    IonicModule,
    RouterModule,
    FormsModule,
    CommonModule
  ]
})
export class InicioPerfilComponent  implements OnInit {

  protected mostrarOpcionesInfoGeneral: boolean = false;
  protected mostrarOpcionesMasInformacion: boolean = false;

  protected informacionPerfil$: Observable<InformacionPerfilDto>;
  protected infoPerfil: InformacionPerfilDto;
  protected fotoPerfilUrl: string;

  protected pacientes: UsuarioExpedienteGridDTO[];

  constructor(
    private router: Router, 
    private authService: AuthService,
    private alertCtrl: AlertController,
    private modalController: ModalController,
    private usuarioService: UsuarioService,
  ) { 
    addIcons({
      chevronForward,
      chevronDown,
      'persona':'assets/img/svg/user.svg',
      'cruz': 'assets/img/svg/cross.svg', 
      'portapapeles-mas': 'assets/img/svg/clipboard-plus.svg',
      'pildora': 'assets/img/svg/pill.svg',
      'cerrar-sesion': 'assets/img/svg/log-out.svg',
      'informacion': 'assets/img/svg/info.svg'
    })
  }

  ngOnInit() {
    this.obtenerInfoPerfil();
  }

  protected navigateInformacionGeneral(){
    this.router.navigateByUrl("home/perfil/informacion-general");
  }

  protected navigateInformacionDomicilio(){
    this.router.navigateByUrl("home/perfil/informacion-general");
  }

  protected navigateInformacionAntecedentes(){
    this.router.navigateByUrl("home/perfil/informacion-general/antecedentes");
  }

  protected navigateInformacionDiagnosticos(){
    this.router.navigateByUrl("home/perfil/informacion-general/diagnosticos");
  }

  protected navigateMisDoctores(){
    this.router.navigateByUrl("home/perfil/mis-doctores");
  }

  protected navigateMisEstudios(){
    this.router.navigateByUrl("home/perfil/mis-estudios");
  }

  protected navigateMisTratamientos(){
    this.router.navigateByUrl("home/perfil/mis-tratamientos");
  }

  private cerrarSesion(){
    this.authService.cerrarSesion();
  }

  protected async presentarAlertaCerrarSesion() {
    const alert = await this.alertCtrl.create({
      header: '¿Seguro que deseas cerrar sesión?',
      message: Constants.ALERT_DELETE,
      cssClass: 'custom-alert-delete',
      buttons: [
        {
          text: 'No, regresar',
          role: 'cancel',
        },
        {
          text: 'Sí, cerrar sesión',
          role: 'confirm',
          handler: ()=> {
            this.cerrarSesion();
          }
          },
      ]
    });

    await alert.present();
  }

  protected DropDownInformacionGeneral(){
    this.mostrarOpcionesMasInformacion = false;
    this.mostrarOpcionesInfoGeneral = !this.mostrarOpcionesInfoGeneral;
  }

  protected DropDownMasInformacion(){
    this.mostrarOpcionesInfoGeneral = false;
    this.mostrarOpcionesMasInformacion = !this.mostrarOpcionesMasInformacion;
  }

  protected async mostrarTerminosYCondiciones() {
    const modal = await this.modalController.create({
      component: TerminosYCondicionesComponent,
    });

    modal.present();
  }

  protected async mostrarInfoLibreriasOpenSource() {
    const modal = await this.modalController.create({
      component: InfoLibreriasOpenSourceComponent,
    });

    modal.present();
  }

  private obtenerInfoPerfil(){

    this.informacionPerfil$ = this.usuarioService.consultarInformacionPerfil();

    this.informacionPerfil$.subscribe({
      next: (info) => {
        this.infoPerfil = info;
        if(this.infoPerfil.imagenBase64 == null){
          this.fotoPerfilUrl = "https://ionicframework.com/docs/img/demos/avatar.svg";
        }
        else{
          this.fotoPerfilUrl = `data:${info.imagenBase64?.archivoMime};base64,` + info.imagenBase64?.archivo;
        }
      },
    });
  }

}
