import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import * as Utileria from '../../../shared/utils/utileria';

import { RestablecerContrasenaService } from '@http/seguridad/restablecer-contrasena.service';
import { SharedModule } from 'primeng/api';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { RouterLink } from '@angular/router';
import { RestablecerContrasenaDto } from '@dtos/seguridad/restablecer-contrasena-dto';

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
  public recuperacionCorrecta = false;

  constructor(
    private restablecerContrasenaService: RestablecerContrasenaService
  ) {}

  ngOnInit() {}

  recuperarContrasena(formulario: NgForm) {
    Utileria.validarCamposRequeridos(formulario);
    if (formulario.invalid) {
      return;
    }

    this.btnSubmit = true;

    this.restablecerContrasenaService
      .restablecerContrasena(this.usuario)
      .subscribe(
        (data) => (this.recuperacionCorrecta = true),
        (error) => (this.btnSubmit = false)
      );
  }
}
