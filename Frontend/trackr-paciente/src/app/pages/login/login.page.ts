import { LoginResponse } from './../../models/seguridad/login-response';
import { LoginRequest } from './../../models/seguridad/login-request';
import { LoginService } from './../../services/seguridad/login.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import * as Utileria from '@shared/utileria';
import { IonicModule } from '@ionic/angular';
import { Router, RouterLink } from '@angular/router';
import { lastValueFrom } from 'rxjs/internal/lastValueFrom';
import { GeneralConstant } from '@shared/general-constant';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    RouterLink
    ]
})
export class LoginPage implements OnInit {
  loginRequest: LoginRequest = new LoginRequest();
  loginResponse: LoginResponse = new LoginResponse();
  btnSubmit: boolean = false;
  constructor(
    private loginService: LoginService,
    private router: Router
    ) { }

  ngOnInit() {
    this.loginRequest = new LoginRequest();
  }

  /**
   * Valida que los campos del formulario sean requeridos y que el correo y la contraseña no estén vacíos.
   * Si es exitoso, llama al método autenticar.
   * @param formulario Formulario a validar.
   * @returns
   */
  public async enviarFormulario(formulario: NgForm){
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
    this.loginRequest.claveTipoUsuario = GeneralConstant.CLAVE_USUARIO_PACIENTE;
    await lastValueFrom(this.loginService.authenticate(this.loginRequest))
          .then((loginResponse: LoginResponse) => {
            localStorage.setItem(GeneralConstant.TOKEN_KEY, loginResponse.token);
            this.router.navigate(['/paciente']);
          })
          .catch(error => {
            localStorage.removeItem(GeneralConstant.TOKEN_KEY);
            this.btnSubmit = false;
          });
  }

}
