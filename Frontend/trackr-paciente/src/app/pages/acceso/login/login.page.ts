import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { LoginService } from '@http/seguridad/login.service';
import { IonicModule, AlertController, LoadingController } from '@ionic/angular';
import { LoginRequest } from '@models/seguridad/login-request';
import { LoginResponse } from '@models/seguridad/login-response';
import { GeneralConstant } from '@utils/general-constant';
import * as Utileria from '@utils/utileria';
import { lastValueFrom } from 'rxjs/internal/lastValueFrom';
import { AuthService } from '../../../auth/auth.service';
import { addIcons } from 'ionicons';
import { BehaviorSubject } from 'rxjs';
import { LoadingSpinnerService } from 'src/app/services/dashboard/loading-spinner.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    RouterLink,
  ]
})
export class LoginPage implements OnInit {
  
  loginRequest: LoginRequest = new LoginRequest();
  loginResponse: LoginResponse = new LoginResponse();
  btnSubmit: boolean = false;
  protected pswInputType: string = "password";
  protected mostrarPwd: boolean = false;

  constructor(
    private loginService: LoginService,
    private router: Router,
    private authService: AuthService,
    private loadingSpinner: LoadingSpinnerService
  ) { 
    addIcons({
      'user': 'assets/img/svg/user.svg',
      'eye': 'assets/img/svg/eye.svg',
      'eye-off': 'assets/img/svg/eye-off.svg',
      'chevron-left': 'assets/img/svg/chevron-left.svg',
      'info': 'assets/img/svg/info.svg',
    })
  }
  
  ngOnInit() {
  }
  /**
   * Valida que los campos del formulario sean requeridos y que el correo y la contraseña no estén vacíos.
   * Si es exitoso, llama al método autenticar.
   * @param formulario Formulario a validar.
   * @returns
   */
  public async enviarFormulario(formulario: NgForm){
    this.loadingSpinner.presentLoading();
    this.btnSubmit = true;

    if(formulario.invalid){
      Utileria.validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      return;
    }

    if(!this.loginRequest.correo || !this.loginRequest.contrasena){
      this.btnSubmit = false;
      return;
    }

    this.autenticarPaciente();
    this.btnSubmit = false;
  }

  /**
   * Realiza la autenticación del usuario utilizando el servicio de inicio de sesión y redirige a la página de pacientes.
   * Llama al método Authenticate de LoginService. Si es exitoso, guarda el LoginResponse Token en el localStorage y redirige a la página de pacientes.
   */
  public async autenticarPaciente() {
    // TODO: 2023-07-18 ->
    this.loginRequest.claveTipoUsuario = GeneralConstant.CLAVE_USUARIO_PACIENTE;
    await lastValueFrom(this.loginService.authenticate(this.loginRequest))
          .then((loginResponse: LoginResponse) => {
            this.authService.guardarToken(loginResponse.token);

            this.loadingSpinner.dismissLoading();
            this.router.navigate(['/home']);
          })
          .catch(error => {
            this.loadingSpinner.dismissLoading();
            this.authService.cerrarSesion(false);
            this.btnSubmit = false;
          });
  }

  protected mostrarContrasena(){
    if(this.mostrarPwd == true){
      this.mostrarPwd = false;
      this.pswInputType = "password";
    }
    else{
      this.mostrarPwd = true;
      this.pswInputType = "text";
    }
  }

}
