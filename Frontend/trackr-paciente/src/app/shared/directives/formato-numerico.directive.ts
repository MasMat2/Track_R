import { Directive } from '@angular/core';
import { Validator, ValidatorFn, AbstractControl, FormControl, NG_VALIDATORS } from '@angular/forms';

function formatoNumerico(): ValidatorFn {
  const pattern = new RegExp(/^[0-9]+$/im);
  return (control: AbstractControl) => {
    if (control.value === undefined || control.value === null || control.value === '') {
      return null;
    } else if (!pattern.test(control.value)) {
      return {
        formatoNumerico: {
          valid: false
        }
      };
    }
    return null;
  };
}

@Directive({
  selector: '[appFormatoNumerico]',
  providers: [{ provide: NG_VALIDATORS, useExisting: FormatoNumericoDirective, multi: true }]
})

/**
 * Valida el formato de los teléfonos.
 */
export class FormatoNumericoDirective implements Validator {
  validator: ValidatorFn;

  constructor() {
    this.validator = formatoNumerico();
  }

  validate(c: FormControl) {
    return this.validator(c);
  }
}
