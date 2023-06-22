import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CheckboxCustomEvent, IonicModule } from '@ionic/angular';
import * as Utileria from '@shared/utileria';
import { Usuario } from 'src/app/models/usuario';
import { UsuarioService } from 'src/app/services/seguridad/usuario.service';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.page.html',
  styleUrls: ['./registro.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule],
})
export class RegistroPage implements OnInit {
  protected usuario = new Usuario();
  protected confirmarContrasena: string = '';
  protected submitting: boolean = false;
  protected termsAccepted: boolean = false;

  constructor(private usuarioService: UsuarioService) {}

  public ngOnInit() {}

  protected enviarFormulario(formulario: NgForm): void {
    this.submitting = true;

    if (!formulario.valid) {
      Utileria.validarCamposRequeridos(formulario);
      this.submitting = false;
      return;
    }

    this.usuario.idTipoUsuario = 2;
    this.usuario.idCompania = 1;
    this.usuario.idPerfil = 1;
    this.agregar();
  }

  protected changeEvent(event: Event) {
    const checkboxEvent = event as CheckboxCustomEvent;
    this.termsAccepted = checkboxEvent.target.checked;
  }

  protected agregar(): Promise<boolean> {
    return this.usuarioService
      .agregar(this.usuario)
      .toPromise()
      .then(() => true)
      .catch(() => false);
  }
}
