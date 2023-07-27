import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  DoCheck,
  SimpleChanges
} from '@angular/core';
import { NgControl, ControlValueAccessor, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-number-input',
  templateUrl: './number-input.component.html',
  styleUrls: ['./number-input.component.scss']
})
export class NumberInputComponent extends NgControl implements OnInit, OnChanges, ControlValueAccessor {
  control: AbstractControl;
  @Input() valor: any = 0;
  @Input() min: any;
  @Input() max: any;
  @Input() required = false;
  @Input() deshabilitado = false;
  @Input() permitirDecimales: any;
  @Input() nameForm = 'default';
  @Input() touchedVar: any;

  @Output() valorChange: EventEmitter<number> = new EventEmitter<number>();
  @Output() touchedVarChange: EventEmitter<any> = new EventEmitter<any>();
  @Input() longitud: number;

  viewToModelUpdate(newValue: any): void {}

  ngOnChanges(changes: SimpleChanges): void {}

  writeValue(obj: any): void {}

  registerOnChange(fn: any): void {}

  registerOnTouched(fn: any): void {
    this.touchedVar = fn;
    this.touchedVarChange.emit(this.touchedVar);
  }

  ngOnInit() {
  }

  onCantidadChange() {
    this.valorChange.emit(this.valor);
  }

  aumentar() {
    if (this.valor === undefined) {
      this.valor = this.min ? this.min - 1 : 0;
    }

    if (this.max === undefined || this.valor < this.max || this.max === null) {
      this.valor++;
    }

    if (this.max !== null && this.valor > this.max) {
      this.valor = this.max;
    }

    this.valorChange.emit(this.valor);
  }

  disminuir() {
    if (this.valor === undefined) {
      this.valor = this.max ? this.max + 1 : 1;
    }

    if (this.min === undefined || this.valor > this.min || this.min === null) {
      this.valor--;
    }

    if (this.valor < this.min) {
      this.valor = this.min;
    }

    this.valorChange.emit(this.valor);
  }
}
