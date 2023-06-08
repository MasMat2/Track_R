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
    if (this.campo.idDominioNavigation.fechaMinima !== null && this.campo.idDominioNavigation.fechaMaxima !== null) {
      setTimeout(() => {
        this.campo.idDominioNavigation.fechaMinima = new Date(this.campo.idDominioNavigation.fechaMinima);
        this.campo.idDominioNavigation.fechaMaxima = new Date(this.campo.idDominioNavigation.fechaMaxima);
        this.campo.idDominioNavigation.fechaMaxima.setHours(23, 59, 59);
      }, 500);
    }

    if (this.campo.idDominioNavigation.tipoCampo === 'Switch' && this.campo.valor === undefined) {
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
    control = new FormControl(this.campo.valor, Validators.compose([
      this.campo.requerido ? Validators.required : null,
      Validators.min(this.campo.idDominioNavigation.valorMinimo),
      Validators.max(this.campo.idDominioNavigation.valorMaximo),
      this.campo.idDominioNavigation.longitudMaxima !== null ? Validators.maxLength(this.campo.idDominioNavigation.longitudMaxima) : null
    ]));

    if(this.campo.idDominioNavigation.fechaMaxima !== null || this.campo.idDominioNavigation.fechaMinima !== null){
      let fechaCampo = new Date(control.value)
      fechaCampo.setHours(0,0,0,0);
      if( (fechaCampo > (new Date(this.campo.idDominioNavigation.fechaMaxima.toString())) )
      || fechaCampo < (new Date(this.campo.idDominioNavigation.fechaMinima.toString()))){
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
    if(this.campo.idDominioNavigation.fechaMaxima !== null){
      if(this.campo.valor && this.campo.valor > this.campo.idDominioNavigation.fechaMaxima){
        this.campo.valor = this.campo.idDominioNavigation.fechaMaxima;
        this.campo.valor.setHours(0,0,0)
      }
    }
    if(this.campo.idDominioNavigation.fechaMinima !== null){
      if(this.campo.valor && this.campo.valor < this.campo.idDominioNavigation.fechaMinima){
        this.campo.valor = this.campo.idDominioNavigation.fechaMinima;
        this.campo.valor.setHours(0,0,1)
      }
    }
  }
}
