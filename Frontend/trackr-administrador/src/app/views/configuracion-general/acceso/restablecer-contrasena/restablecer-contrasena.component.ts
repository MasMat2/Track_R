import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { RestablecerContrasenaDto } from '@dtos/seguridad/restablecer-contrasena-dto'
import {RestablecerContrasenaService} from '@http/seguridad/restablecer-contrasena.service'
import * as Utileria from '@utils/utileria';
import { MensajeService } from '../../../../shared/components/mensaje/mensaje.service';

@Component({
  selector: 'app-restablecer-contrasena',
  templateUrl: './restablecer-contrasena.component.html',
  styleUrls: ['./restablecer-contrasena.component.scss'],
  standalone: true,
  imports : [
    CommonModule,
    FormsModule
  ]
})
export class RestablecerContrasenaComponent implements OnInit {

  protected restablecerContrasena = new RestablecerContrasenaDto;
  protected valido = false;
  protected btnSubmit = false;
  protected confirmarContrasena = '';


  constructor(
    private restablecerContrasenaService: RestablecerContrasenaService,
    private route: ActivatedRoute,
    private mensajeService: MensajeService
  ){ }


  ngOnInit(){
    this.route.queryParams.subscribe(params => {
      this.restablecerContrasena.correo = params['id'];
      this.restablecerContrasena.clave = params['tkn'];
    });

    this.validarActualizacion(this.restablecerContrasena);

  }

  validarActualizacion(datos: RestablecerContrasenaDto){
    this.restablecerContrasenaService.validarActualizarContrasena(datos).subscribe({
      next: (res) => {
        this.valido = res;
      },
      error : (error) => {

      }
    })

  }

  procesarActualizacion(datos: RestablecerContrasenaDto){
    this.restablecerContrasenaService.procesarActualizarContrasena(datos).subscribe({
      complete: () => {
        this.mensajeService.modalExito("Contraseña actualizada con éxito");
      }
    })
  }

  enviarFormulario(formulario:NgForm){
    this.btnSubmit = true;

    if(!formulario.valid){
      Utileria.validarCamposRequeridos(formulario);
      this.btnSubmit = false;
    }

    this.procesarActualizacion(this.restablecerContrasena);
  }

}
