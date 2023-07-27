import {
  Component,
  OnInit,
  Input,
  forwardRef,
  ChangeDetectorRef,
  AfterViewChecked,
  Output,
  EventEmitter,
} from '@angular/core';
import {
  ControlValueAccessor,
  NG_VALUE_ACCESSOR,
  AbstractControl,
  ValidationErrors,
  Validators,
  FormControl,
  NG_ASYNC_VALIDATORS,
  AsyncValidator,
} from '@angular/forms';
import { Observable, of } from 'rxjs';
import { SeccionCampo } from '@models/gestion-entidad/seccion-campo';
import { GeneralConstant } from '@utils/general-constant';

@Component({
  selector: 'app-campo-expediente',
  templateUrl: './campo-expediente.component.html',
  styleUrls: ['./campo-expediente.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => CampoExpedienteComponent),
      multi: true,
    },
    {
      provide: NG_ASYNC_VALIDATORS,
      useExisting: forwardRef(() => CampoExpedienteComponent),
      multi: true,
    },
  ],
})
export class CampoExpedienteComponent
  implements
    OnInit,
    ControlValueAccessor,
    AfterViewChecked,
    AsyncValidator {

  @Input() campo: SeccionCampo;
  @Input() classSwitch: string = 'divSwitchNormal';
  @Input() par: boolean;
  @Input() touched: boolean;

  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;
  public configDate = GeneralConstant.CONFIG_DATEPICKER;
  public configLista = Object.assign(
    {
      labelField: 'valor',
      valueField: 'idDominioDetalle',
      searchField: ['valor'],
    },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  public configListaMultiple = Object.assign({
      labelField: 'valor',
      valueField: 'idDominioDetalle',
      searchField: ['valor'],
      plugins: ['dropdown_direction', 'remove_button'],
      maxItems: null,
      dropdownDirection: 'down',
      dropdownParent: 'body'
    }
  );

  onChanged: any = () => {};
  onTouched: any = () => {};
  onValidationChange: any = () => {};

  @Output() change = new EventEmitter<any>();

  private _value: string;

  get value() {
    return this._value;
  }

  set value(value: any) {
    this._value = value;

    this.onChanged(this._value);
    this.onValidationChange();
  }

  constructor(private cdRef: ChangeDetectorRef) {}

  ngAfterViewChecked() {
    this.cdRef.detectChanges();
  }

  ngOnInit() {
    const dominio = this.campo.idDominioNavigation;

    if (
      dominio.fechaMinima !== null &&
      dominio.fechaMaxima !== null
    ) {
      setTimeout(() => {
        dominio.fechaMinima = new Date(dominio.fechaMinima);
        dominio.fechaMaxima = new Date(dominio.fechaMaxima);
        dominio.fechaMaxima.setHours(23, 59, 59);
      }, 500);
    }

    if (
      dominio.tipoCampo === 'Switch' &&
      this.campo.valor === undefined
    ) {
      this.campo.valor = false;
    }
  }

  onCheckboxSelected($event:any, opcion:any) {}

  writeValue(obj: any): void {
    this._value = obj;
    this.change.emit(this.campo);
  }

  registerOnChange(fn: any): void {
    this.onChanged = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {}

  validate(control: AbstractControl): Observable<ValidationErrors | null> {
    const validationErrors = this._validateInternal(control);

    // En lugar de devolver null, devolvemos un Observable que emite null.
    return of(validationErrors || null);
}
  _validateInternal(control: AbstractControl): ValidationErrors | null {
    const dominio = this.campo.idDominioNavigation;

    control = new FormControl(
      this.campo.valor,
      Validators.compose([
        this.campo.requerido
          ? Validators.required
          : null,
        dominio.permiteFueraDeRango
          ? null
          : Validators.min(dominio.valorMinimo),
        dominio.permiteFueraDeRango
          ? null
          : Validators.max(dominio.valorMaximo),
        dominio.longitudMaxima !== null
          ? Validators.maxLength(dominio.longitudMaxima)
          : null,
      ])
    );

    if (dominio.fechaMaxima !== null || dominio.fechaMinima !== null) {
      const fechaCampo = new Date(control.value);
      fechaCampo.setHours(0,0,0,0);

      const fechaMaxima = new Date(dominio.fechaMaxima.toString());
      const fechaMinima = new Date(dominio.fechaMinima.toString());

      if( fechaCampo > fechaMaxima || fechaCampo < fechaMinima){
        return  { dateInvalid: true }
      }
    }

    return  control.errors;
  }

  registerOnValidatorChange?(fn: () => void): void {
    this.onValidationChange = fn;
  }

  onSwitchChanged(value:boolean){
    this.campo.valor = value;
  }

  onSelectMultipleChange(value: number[]) {

    const valores = value.map(i => Number(i));

    this.campo.idDominioNavigation.dominioDetalle.forEach(c => {
      if (valores.includes(c.idDominioDetalle)) {
        c.seleccionada = true;
      } else {
        c.seleccionada = false;
      }
    });
  }

  cambioFecha() {
    const dominio = this.campo.idDominioNavigation;

    if(dominio.fechaMaxima !== null){
      if(this.campo.valor && this.campo.valor > dominio.fechaMaxima){
        this.campo.valor = dominio.fechaMaxima;
        this.campo.valor.setHours(0,0,0)
      }
    }
    if(dominio.fechaMinima !== null){
      if(this.campo.valor && this.campo.valor < dominio.fechaMinima){
        this.campo.valor = dominio.fechaMinima;
        this.campo.valor.setHours(0,0,1)
      }
    }
  }
}
