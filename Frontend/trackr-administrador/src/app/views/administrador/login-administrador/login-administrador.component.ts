import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginRequest } from 'src/app/models/seguridad/login-request';
import { LoginResponse } from 'src/app/models/seguridad/login-response';
import { LoginService } from 'src/app/shared/services/seguridad/login.service';
import { Router } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import * as Utileria from 'src/app/shared/utileria';
import { GeneralConstant } from 'src/app/shared/general-constant';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-login-administrador',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './login-administrador.component.html',
  styleUrls: ['./login-administrador.component.scss']
})
export class LoginAdministradorComponent implements OnInit {
  loginRequest: LoginRequest = new LoginRequest();
  loginResponse: LoginResponse = new LoginResponse();
  btnSubmit: boolean = false;
  constructor(
    private loginService: LoginService,
    private router: Router,
  ) { }

  ngOnInit(): void {

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
    this.loginRequest.claveTipoUsuario = GeneralConstant.CLAVE_USUARIO_ADMINISTRADOR;
    await lastValueFrom(this.loginService.authenticate(this.loginRequest))
          .then((loginResponse: LoginResponse) => {
            localStorage.setItem(GeneralConstant.TOKEN_KEY, loginResponse.token);
            this.router.navigate(['/administrador']);
          })
          .catch(error => {
            localStorage.removeItem(GeneralConstant.TOKEN_KEY);
            this.btnSubmit = false;
          });
  }

}
