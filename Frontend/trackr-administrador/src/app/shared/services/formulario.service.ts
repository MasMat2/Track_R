import { Injectable } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MensajeService } from '../components/mensaje/mensaje.service';
import { GeneralConstant } from '../utils/general-constant';

@Injectable({
  providedIn: 'root',
})
export class FormularioService {
  constructor(private modalMensajeService: MensajeService) {}

  validarCamposRequeridos(formulario: NgForm) {
    Object.keys(formulario.controls).forEach((nombre) => {
      const control = formulario.controls[nombre];
      control.markAsTouched({ onlySelf: true });
      control.markAsDirty({ onlySelf: true });
    });
  }

  validarCamposRequeridosConMensaje(formulario: NgForm) {

    Object.keys(formulario.controls).forEach((nombre) => {
      const control = formulario.controls[nombre];
      control.markAsTouched({ onlySelf: true });
      control.markAsDirty({ onlySelf: true });
    });

    Object.keys(formulario.controls).some((nombre) => {
      const control = formulario.controls[nombre];

      if (control.errors && control.errors['required'] && control.invalid) {
        this.modalMensajeService.modalError(GeneralConstant.MENSAJE_REQUERIDOS);
        return true;
      }

      return false;
    });
  }

  markAsUntouched(formulario: NgForm) {
    Object.keys(formulario.controls).forEach((nombre) => {
      const control = formulario.controls[nombre];
      control.markAsUntouched({ onlySelf: true });
    });
  }
}
