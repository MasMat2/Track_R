import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { IonicModule, ModalController } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { AuthService } from '../../../../auth/auth.service';
import { AlertController } from '@ionic/angular/standalone';
import { Constants } from '@utils/constants/constants';
import { Observable } from 'rxjs';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { UsuarioExpedienteGridDTO } from 'src/app/shared/Dtos/seguridad/usuario-expediente-grid-dto copy';
import { InformacionPerfilDto } from 'src/app/shared/Dtos/perfil/informacion-perfil-dto';
import { TerminosYCondicionesComponent } from '@sharedComponents/terminos-y-condiciones/terminos-y-condiciones.component';
import { InfoLibreriasOpenSourceComponent } from '@sharedComponents/info-librerias-opensource/info-librerias-opensource.component';
import { AvisoPrivacidadComponent } from '@sharedComponents/aviso-privacidad/aviso-privacidad.component';


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

  protected informacionPerfil$: Observable<InformacionPerfilDto>;
  protected infoPerfil: InformacionPerfilDto;
  protected fotoPerfilUrl: string = "assets/img/svg/avatar-placeholder.svg";

  protected pacientes: UsuarioExpedienteGridDTO[];

  constructor(
    private router: Router, 
    private authService: AuthService,
    private alertCtrl: AlertController,
    private modalController: ModalController,
    private usuarioService: UsuarioService,
  ) { 
    addIcons({
      'persona':'assets/img/svg/user.svg',
      'cruz': 'assets/img/svg/cross.svg', 
      'portapapeles-mas': 'assets/img/svg/clipboard-plus.svg',
      'pildora': 'assets/img/svg/pill.svg',
      'cerrar-sesion': 'assets/img/svg/log-out.svg',
      'informacion': 'assets/img/svg/info.svg',
      'chevron-down': 'assets/img/svg/chevron-down.svg',
      'chevron-right': 'assets/img/svg/chevron-right.svg',

    })
  }

  ngOnInit() {
    this.obtenerInfoPerfil();
  }

  protected navigateInformacionGeneral(){
    this.router.navigateByUrl("home/perfil/informacion-general");
  }

  protected navigateInformacionDomicilio(){
    this.router.navigateByUrl("home/perfil/informacion-general/domicilio");
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
      header: '¿Seguro(a) que deseas cerrar sesión?',
      cssClass: 'custom-alert color-error icon-info two-buttons',
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
  
  protected async mostrarTerminosYCondiciones() {
    const modal = await this.modalController.create({
      component: TerminosYCondicionesComponent,
    });

    modal.present();
  }
  protected async mostrarAvisoPrivacidad() {
    const modal = await this.modalController.create({
      component: AvisoPrivacidadComponent,
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
        if(this.infoPerfil?.imagenBase64.archivo != null){
          this.fotoPerfilUrl = `data:${info.imagenBase64?.archivoMime};base64,` + info.imagenBase64?.archivo;
        }
      },
    });
  }

}
