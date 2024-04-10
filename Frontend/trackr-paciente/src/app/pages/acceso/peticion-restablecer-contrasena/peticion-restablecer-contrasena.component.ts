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
import { chevronBack, personOutline} from 'ionicons/icons';

import { Constants } from '@utils/constants/constants';
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
  protected spinner: string = Constants.ALERT_SPINNER;

  //Estado de "cargando" para mostrar el alert con spinner
  cargandoSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  cargando$ = this.cargandoSubject.asObservable();

  constructor(
    private restablecerContrasenaService: RestablecerContrasenaService,
    private alertController: AlertController,
    private router: Router,
  ) { addIcons({chevronBack, personOutline}) }

  ngOnInit() {}

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
        //this.recuperacionCorrecta = true;
        this.cargandoSubject.next(false);
        this.presentAlert();
      },
    })
    
  }

  protected async presentAlert() {

    const alertSuccess = await this.alertController.create({
      header: '¡Revisa tu correo!',
      subHeader: 'Acabamos de enviarte un correo electrónico con un enlace para restablecer tu contraseña.',
      message: Constants.ALERT_SUCCESS,
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
