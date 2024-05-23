import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import * as Utileria from '../../../shared/utils/utileria';

import { RestablecerContrasenaService } from '@http/seguridad/restablecer-contrasena.service';
import { SharedModule } from 'primeng/api';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { CommonModule } from '@angular/common';
import { IonicModule, AlertController } from '@ionic/angular';
import { Router, RouterLink } from '@angular/router';
import { RestablecerContrasenaDto } from '@dtos/seguridad/restablecer-contrasena-dto';
import { addIcons } from 'ionicons';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-peticion-restablecer-contrasena',
  templateUrl: './peticion-restablecer-contrasena.component.html',
  styleUrls: ['./peticion-restablecer-contrasena.component.scss'],
  standalone: true,
  imports: [
    SharedModule,
    DirectiveModule,
    CommonModule,
    IonicModule,
    FormsModule,
    RouterLink,
  ],
  providers: [RestablecerContrasenaService],
})
export class PeticionRestablecerContrasenaComponent implements OnInit {
  public usuario = new RestablecerContrasenaDto();
  public btnSubmit = false;
  //public recuperacionCorrecta = false;

  //Estado de "cargando" para mostrar el alert con spinner
  private cargandoSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private cargando$ = this.cargandoSubject.asObservable();
  private loading: any;

  constructor(
    private restablecerContrasenaService: RestablecerContrasenaService,
    private alertController: AlertController,
    private router: Router,
  ) { addIcons({
    'user': 'assets/img/svg/user.svg',
  }) }

  async presentLoading() {
    this.loading = await this.alertController.create({
      cssClass: "custom-alert-loading"
    })
    return await this.loading.present();
  }

  async dismissLoading() {
    if (this.loading) {
      await this.loading.dismiss();
      this.loading = null;
    }
  }


  ngOnInit() {
    this.cargando$.subscribe(cargando => {
      if (cargando) {
        this.presentLoading();
      } else {
        this.dismissLoading();
      }
    });
  }

  recuperarContrasena(formulario: NgForm) {
    this.cargandoSubject.next(true);
    this.btnSubmit = true;

    Utileria.validarCamposRequeridos(formulario);
    if (formulario.invalid) {
      return;
    }

    this.restablecerContrasenaService.restablecerContrasena(this.usuario).subscribe({
      next: (a)=> {
      },
      error: () => {
        this.cargandoSubject.next(false);
      },
      complete: ()=> {
        this.cargandoSubject.next(false);
        this.presentAlert();
      },
    })
    
  }

  protected async presentAlert() {

    const alertSuccess = await this.alertController.create({
      header: '¡Revisa tu correo!',
      subHeader: 'Acabamos de enviarte un correo electrónico con un enlace para restablecer tu contraseña.',
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm',
        handler: () => {
          this.router.navigateByUrl('/acceso/login');
        }
      }],
      cssClass: 'custom-alert-success',
    });

    await alertSuccess.present();
  }
}
