import { Directive } from '@angular/core';
import { Validator, ValidatorFn, AbstractControl, FormControl, NG_VALIDATORS } from '@angular/forms';

function formatoNombre(): ValidatorFn {
  const pattern = new RegExp(/^[a-zA-ZÀ-ÿ\u00f1\u00d1\u0027 ]*$/im);
  return (control: AbstractControl) => {
    if (control.value === undefined || control.value === null || control.value === '') {
      return null;
    } else {
      if (control.value.trim() === '') {
        return {
          formatoNombre: {
            valid: false
          }
        };
      }
      if (!pattern.test(control.value)) {
        return {
          formatoNombre: {
            valid: false
          }
        };
      }
    }
    return null;
  };
}

@Directive({
  selector: '[appFormatoNombre]',
  providers: [{ provide: NG_VALIDATORS, useExisting: FormatoNombreDirective, multi: true }]
})

/**
 * Valida el formato de nombre.
 */
export class FormatoNombreDirective implements Validator {
  validator: ValidatorFn;

  constructor() {
    this.validator = formatoNombre();
  }

  validate(c: FormControl) {
    return this.validator(c);
  }
}
