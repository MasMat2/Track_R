import { Directive } from '@angular/core';
import { Validator, ValidatorFn, AbstractControl, FormControl, NG_VALIDATORS } from '@angular/forms';

function formatoCorreo(): ValidatorFn {
  const pattern = new RegExp(
    /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/im
  );
  return (control: AbstractControl) => {
    if (control.value === undefined || control.value === null || control.value === '') {
      return null;
    } else if (!pattern.test(control.value)) {
      return {
        formatoCorreo: {
          valid: false
        }
      };
    }
    return null;
  };
}

@Directive({
  selector: '[appFormatoCorreo]',
  providers: [{ provide: NG_VALIDATORS, useExisting: FormatoCorreoDirective, multi: true }]
})

/**
 * Valida el formato de los teléfonos.
 */
export class FormatoCorreoDirective implements Validator {
  validator: ValidatorFn;

  constructor() {
    this.validator = formatoCorreo();
  }

  validate(c: FormControl) {
    return this.validator(c);
  }
}
