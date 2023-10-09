import { Component, OnInit } from '@angular/core';
import * as Utileria from '../../../shared/utils/utileria';
import { FormsModule, NgForm } from '@angular/forms';
import { RestablecerContrasenaService } from '@http/seguridad/restablecer-contrasena.service';
import { SharedModule } from 'primeng/api';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { ActivatedRoute, Router, Params, RouterLink } from '@angular/router';
import { RestablecerContrasenaDto } from '@dtos/seguridad/restablecer-contrasena-dto';
@Component({
  selector: 'app-restablecer-contrasena',
  templateUrl: './restablecer-contrasena.component.html',
  styleUrls: ['./restablecer-contrasena.component.scss'],
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
export class RestablecerContrasenaComponent implements OnInit {
  btnSubmit = false;
  usuario: RestablecerContrasenaDto;
  confirmacionContrasena: string;
  contrasenaRestablecida = false;
  validacionCorrecta = false;

  constructor(
    private restablecerContrasenaService: RestablecerContrasenaService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.usuario = new RestablecerContrasenaDto();
  }

  ngOnInit() {
    this.route.queryParams.subscribe((parametros: Params) => {
      const id = parametros['id'];
      const tkn = parametros['tkn'];
      this.validarParametrosNecesarios({ id, tkn });
      this.validarActualizarContrasena({ id, tkn });
    });
  }

  redirigirInicio() {
    this.router.navigate(['/login-administrador']);
    return;
  }

  validarParametrosNecesarios(parametros: { id: string; tkn: string }) {
    if (!parametros || !parametros.id || !parametros.tkn) {
      this.redirigirInicio();
    }
  }

  validarActualizarContrasena(parametros: { id: string; tkn: string }) {
    this.usuario.correo = parametros.id;
    this.usuario.clave = parametros.tkn;
    this.restablecerContrasenaService
      .validarActualizarContrasena(this.usuario)
      .subscribe(
        (result) => {
          if (!result) {
            this.redirigirInicio();
          }

          this.validacionCorrecta = result;
        },
        (error) => this.redirigirInicio()
      );
  }

  restablecerPassword(formulario: NgForm) {
    Utileria.validarCamposRequeridos(formulario);
    if (!formulario.valid) {
      return;
    }
    this.btnSubmit = true;

    this.restablecerContrasenaService
      .procesarActualizacionContrasena(this.usuario)
      .subscribe(
        (data) => {
          this.contrasenaRestablecida = true;
        },
        (error) => (this.btnSubmit = false)
      );
  }
}
