import { Directive, forwardRef, Attribute } from '@angular/core';
import { Validator, AbstractControl, NG_VALIDATORS } from '@angular/forms';

@Directive({
  selector:
    '[appConfirmacionContrasena][formControlName],[appConfirmacionContrasena][formControl],[appConfirmacionContrasena][ngModel]',
  providers: [
    { provide: NG_VALIDATORS, useExisting: forwardRef(() => ConfirmacionContrasenaDirective), multi: true }
  ]
})
export class ConfirmacionContrasenaDirective implements Validator {
  constructor(
    @Attribute('appConfirmacionContrasena') public confirmarContrasena: string,
    @Attribute('reversa') public reversa: string
  ) {}
  validate(control: AbstractControl): { [key: string]: any } | null {
    let v = control.value;
    const e = control.root.get(this.confirmarContrasena);

    if (v === null) {
      v = '';
    }

    if (e && e.value === null) {
      e.setValue('');
    }

    if (e && v !== e.value && !this.reversa) {
      return {
        confirmacionContrasena: { valido: false }
      };
    }

    if (e && v === e.value && this.reversa) {
      if (e.errors) {
        delete e.errors['confirmacionContrasena'];
      }

      if (e.errors && Object.keys(e.errors).length == 0) {
        e.setErrors(null);
      }
    }

    if (e && e.value !== '' && v !== e.value && this.reversa) {
      e.setErrors({ confirmacionContrasena: { valido: false } });
    }

    return null;
  }

  esReversa() {
    if (!this.reversa) {
      return false;
    }
    return this.reversa === 'true' ? true : false;
  }
}
