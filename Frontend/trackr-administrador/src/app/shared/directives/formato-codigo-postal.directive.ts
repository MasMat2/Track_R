import { Directive, Input } from '@angular/core';
import { AbstractControl, FormControl, NG_VALIDATORS, ValidationErrors, Validator, ValidatorFn } from '@angular/forms';

function formatoCodigoPostal(codigoPostalExtranjero: boolean): ValidatorFn {
  const patternNacional = new RegExp(/^[0-9]{1,5}$/im);
  const patternExtranjero = new RegExp(/^[A-Za-z0-9]+$/im);

  return (control: AbstractControl) => {
    if (control.value === undefined || control.value === null || control.value === '') {
      return null;
    } else if (codigoPostalExtranjero) {
      if (!patternExtranjero.test(control.value)) {
        return {
          formatoCodigoPostal: {
            valid: false
          },
          extranjero: true
        };
      }
    } else {
      if (!patternNacional.test(control.value)) {
        return {
          formatoCodigoPostal: {
            valid: false
          },
          extranjero: false
        };
      }
    }
    return null;
  };
}

@Directive({
  selector: '[appFormatoCodigoPostal]',
  providers: [{ provide: NG_VALIDATORS, useExisting: FormatoCodigoPostalDirective, multi: true }]
})
export class FormatoCodigoPostalDirective implements Validator {
  private _onChange?: () => void;
  private _esExtranjero: boolean;

  @Input()
  get esExtranjero() {
    return this._esExtranjero;
  }

  set esExtranjero(value: boolean) {
    this._esExtranjero = value;

    if (this._onChange) {
      this._onChange();
    }
  }

  constructor() {  }

  public validate(c: FormControl): ValidationErrors | null {
    const validationFunction = formatoCodigoPostal(this._esExtranjero);
    return validationFunction(c);
  }

  registerOnValidatorChange(fn: () => void): void {
    this._onChange = fn;
  }
}
