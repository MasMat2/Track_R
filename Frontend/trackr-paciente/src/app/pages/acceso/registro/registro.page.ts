import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { AlertController, CheckboxCustomEvent, IonicModule } from '@ionic/angular';
import { Usuario } from '@models/usuario';
import * as Utileria from '@utils/utileria';
import { UsuarioNuevoTrackrDto } from 'src/app/shared/Dtos/seguridad/usuario-nuevo-trackr-dto';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.page.html',
  styleUrls: ['./registro.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule],
})
export class RegistroPage implements OnInit {
  protected usuario = new UsuarioNuevoTrackrDto();
  protected confirmarContrasena: string = '';
  protected submitting: boolean = false;
  protected termsAccepted: boolean = false;

  constructor(
    private usuarioService: UsuarioService,
    private router: Router,
    private alertController: AlertController
  ) {}

  public ngOnInit() {}

  protected enviarFormulario(formulario: NgForm): void {
    this.submitting = true;

    if (!formulario.valid) {
      Utileria.validarCamposRequeridos(formulario);
      this.submitting = false;
      return;
    }

    this.agregar();
  }

  protected changeEvent(event: Event) {
    const checkboxEvent = event as CheckboxCustomEvent;
    this.termsAccepted = checkboxEvent.target.checked;
  }

  private agregar(){
    this.usuarioService.agregarTrackr(this.usuario).subscribe({
      next: () => {
        this.presentAlert();
        this.submitting = false;
      }
    })
  }

  private async presentAlert() {
    const alert = await this.alertController.create({
      header: 'Usuario registrado',
      message: 'Ya puede iniciar sesión, pero debe verificar su correo para usar la app adecuadamente',
      buttons: [{
        text: 'Ok',
        handler: () => {
          this.router.navigate(['/acceso/login']);
        }
      }]
    });

    await alert.present();
  }
}
