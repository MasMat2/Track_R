import { Directive } from '@angular/core';
import { Validator, ValidatorFn, AbstractControl, FormControl, NG_VALIDATORS } from '@angular/forms';

function formatoRfc(): ValidatorFn {
  const pattern = new RegExp(
    /^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$/im
  );
  return (control: AbstractControl) => {
    if (control.value === undefined || control.value === null || control.value === '') {
      return null;
    } else if (!pattern.test(control.value)) {
      return {
        formatoRfc: {
          valid: false
        }
      };
    }
    return null;
  };
}

@Directive({
  selector: '[appFormatoRfc]',
  providers: [{ provide: NG_VALIDATORS, useExisting: FormatoRfcDirective, multi: true }]
})

/**
 * Valida el formato de los teléfonos.
 */
export class FormatoRfcDirective implements Validator {
  validator: ValidatorFn;

  constructor() {
    this.validator = formatoRfc();
  }

  validate(c: FormControl) {
    return this.validator(c);
  }
}
