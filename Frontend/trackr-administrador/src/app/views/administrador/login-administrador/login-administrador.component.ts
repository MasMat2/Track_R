import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginRequest } from '@models/seguridad/login-request';
import { LoginResponse } from '@models/seguridad/login-response';
import { UsuarioLocacion } from '@models/seguridad/usuario-locacion';
import { NgSelectModule } from '@ng-select/ng-select';
import { LoginService } from '@services/seguridad/login.service';
import { GeneralConstant } from '@utils/general-constant';
import * as Utileria from '@utils/utileria';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-login-administrador',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NgSelectModule
  ],
  templateUrl: './login-administrador.component.html',
  styleUrls: ['./login-administrador.component.scss']
})
export class LoginAdministradorComponent implements OnInit {
  protected loginRequest: LoginRequest = new LoginRequest();
  protected loginResponse: LoginResponse = new LoginResponse();
  protected btnSubmit: boolean = false;
  protected mostrarLocaciones: boolean = false;
  protected locaciones: UsuarioLocacion[] = [];

  protected readonly placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  protected readonly placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

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

    this.autenticar();
    this.btnSubmit = false;
  }

  /**
   * Realiza la autenticación del usuario utilizando el servicio de inicio de sesión y redirige a la página de pacientes.
   * Llama al método Authenticate de LoginService. Si es exitoso, guarda el LoginResponse Token en el localStorage y redirige a la página de pacientes.
   */
  public async autenticar() {
    this.loginRequest.claveTipoUsuario = GeneralConstant.CLAVE_USUARIO_ADMINISTRADOR;
    await lastValueFrom(this.loginService.authenticate(this.loginRequest))
      .then((loginResponse: LoginResponse) => {
        localStorage.setItem(GeneralConstant.TOKEN_KEY, loginResponse.token);
        this.router.navigate(['/administrador']);
      })
      .catch(error => {
        if (error.status === 400) {
          this.mostrarLocaciones = true;
          this.locaciones = error.error.locaciones;
        }

        localStorage.removeItem(GeneralConstant.TOKEN_KEY);
        this.btnSubmit = false;
      });
  }
}
