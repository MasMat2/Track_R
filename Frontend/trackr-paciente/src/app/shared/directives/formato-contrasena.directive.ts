import { ValidatorFn, NG_VALIDATORS, Validator, FormControl, AbstractControl } from '@angular/forms';
import { Directive } from '@angular/core';

function formatoContrasena(): ValidatorFn {
  const regexNumeros = /[0-9]+/;
  const regesMayusculas = /[A-Z]+/;

  return (control: AbstractControl) => {
    if (control.value !== undefined && control.value !== null && control.value !== '') {
      const error: any = { formatoContrasena: {} };
      if (control.value.length < 6) {
        // Longitud minima de 6 caracteres
        error.formatoContrasena.longitud = { invalid: true, size: 6 };
        return error;
      } else {
        error.formatoContrasena.longitud = null;
      }

      if (!regexNumeros.test(control.value)) {
        // Incluir minimo un numero
        error.formatoContrasena.numeros = true;
        return error;
      } else {
        error.formatoContrasena.numeros = false;
      }

      if (!regesMayusculas.test(control.value)) {
        // Incluir una letra mayuscula
        error.formatoContrasena.mayuscula = true;
        return error;
      }
    }

    return null;
  };
}

@Directive({
  selector: '[appFormatoContrasena]',
  providers: [{ provide: NG_VALIDATORS, useExisting: FormatoContrasenaDirective, multi: true }],
})
export class FormatoContrasenaDirective implements Validator {
  validator: ValidatorFn;

  constructor() {
    this.validator = formatoContrasena();
  }

  validate(control: FormControl) {
    return this.validator(control);
  }
}
