/*
 * permitir-solo-numeros.ts   1.0 13/06/17
 *
 * Copyright (c) Centro para el Desarrollo de la Industria
 * de Software.
 */

import { Directive, HostListener, ElementRef, Input, OnInit, SimpleChanges, OnChanges } from '@angular/core';
import * as _ from 'lodash';

@Directive({
  selector: '[appPermitirSoloNumeros]'
})
/**
 * Directiva que valida un input de tipo number, permite que solo se introduzcan
 * números. En caso de capturar cualquier otro caracter que no sea número,
 * no permitirá ingresarlo al input. Tampoco permitiá pegar o soltar (paste, drop)
 * en el input si no son números.
 */
export class PermitirSoloNumerosDirective implements OnInit {
  @Input() public value: any;

  @Input('permitirDecimales')
  public permitirDecimales: any;

  @Input('maximo')
  public maximo: number;

  @Input('minimo')
  public minimo: number;

  @Input('longitud')
  public longitud: number;

  private KEYS_PERMITIDOS = ['Backspace', 'Tab', 'Delete', 'ArrowRight', 'ArrowLeft', 'ArrowUp', 'ArrowDown'];
  private readonly REGEX_ENTEROS: RegExp = new RegExp(/^[0-9]*$/);
  private readonly REGEX_DECIMALES: RegExp = new RegExp(/^[0-9]*([\.]{1,1}[0-9]{0,2}){0,1}$/);
  private readonly KEY_PUNTO = '.';
  private readonly KEY_GUION = '-';
  private readonly KEY_SUMA = '+';
  private readonly KEY_V = 'V';
  private regex: RegExp;

  constructor(private el: ElementRef) {}

  /**
   * Detecta las entradas en el input y valida que se esten ingresando solamente
   * números y no otro tipo de caracteres.
   */
  @HostListener('keydown', ['$event'])
  cambiarAMinuscula($event: KeyboardEvent) {
    if (this.KEYS_PERMITIDOS.indexOf($event.key) !== -1) {
      return;
    }

    if (($event.ctrlKey || $event.metaKey) && $event.key.toUpperCase() === this.KEY_V) {
      return;
    }
  }

  /**
   * Detecta que al momento de pegar en el input el clipboard contenga
   * solamente números y no algún otro tipo de caracter.
   */
  @HostListener('paste', ['$event'])
  preventPaste(event: ClipboardEvent) {
    if (!event.clipboardData) {
      return;
    }

    if (!this.regex.test(event.clipboardData.getData('text/plain'))) {
      event.preventDefault();
    }
  }

  /**
   * Detecta que al momento de soltar algo en el input (drop) se tengan solamente
   * números y no algún otro tipo de caracter.
   */
  @HostListener('drop', ['$event'])
  preventDrop(event: DragEvent) {
    if (!event.dataTransfer) {
      return;
    }

    if (!this.regex.test(event.dataTransfer.getData('text'))) {
      event.preventDefault();
    }
  }

  @HostListener('keydown', ['$event'])
  preventSymbolsAndLength($event: KeyboardEvent) {
    const position = this.el.nativeElement.selectionStart;
    const current: string = this.el.nativeElement.value;
    const preValueInput: string = [
      current.slice(0, position),
      $event.key == 'Decimal' ? '.' : $event.key,
      current.slice(position)
    ].join('');

    if (this.KEYS_PERMITIDOS.indexOf($event.key) === -1) {
      if (preValueInput && !String(preValueInput).match(this.regex)) {
        $event.preventDefault();
      } else {
        if (
          $event.key === this.KEY_GUION &&
          (preValueInput === '' || _.includes(preValueInput, this.KEY_GUION))
        ) {
          $event.preventDefault();
        }
        if (
          $event.key === this.KEY_SUMA &&
          (preValueInput === '' || _.includes(preValueInput, this.KEY_SUMA))
        ) {
          $event.preventDefault();
        }
      }

      if (
        (this.maximo && +preValueInput >= this.maximo) ||
        (this.longitud && preValueInput.length > this.longitud)
      ) {
        $event.preventDefault();
      }
    }
  }

  ngOnInit() {
    if (this.permitirDecimales) {
      this.regex = this.REGEX_DECIMALES;
    } else {
      this.regex = this.REGEX_ENTEROS;
    }
  }
}
