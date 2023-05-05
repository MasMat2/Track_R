import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { Usuario } from 'src/app/models/usuario';
import { UsuarioService } from 'src/app/services/seguridad/usuario.service';
import * as Utileria from "@shared/utileria";

@Component({
  selector: 'app-registro',
  templateUrl: './registro.page.html',
  styleUrls: ['./registro.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule]
})
export class RegistroPage implements OnInit {
  usuario = new Usuario;
  public disableSubmit: boolean = false;
  public confirmarContrasena: string = '';

  constructor(
    private usuarioService: UsuarioService,
  ) { }

  ngOnInit() {
  }

  public enviarFormulario(formulario: NgForm): void {
    this.disableSubmit = true;

    if (!formulario.valid) {
        Utileria.validarCamposRequeridos(formulario);
        this.disableSubmit = false;
        return;
    }

    this.usuario.idTipoUsuario = 2;
    this.usuario.idCompania = 1;
    this.usuario.idPerfil = 1;
    this.agregar();
}

protected agregar(): Promise<boolean> {
    return this.usuarioService
        .agregar(this.usuario)
        .toPromise()
        .then(() => true)
        .catch(() => false);
}

}
