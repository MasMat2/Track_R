import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[appMaxDigits]'
})
export class MaxDigitsDirective {
  @Input() appMaxDigits: number;

  constructor(private el: ElementRef) {}

  @HostListener('input', ['$event']) onInputChange(event : Event) {
    const initialValue = this.el.nativeElement.value;
    this.el.nativeElement.value = initialValue.slice(0, this.appMaxDigits) || '';
    if (initialValue !== this.el.nativeElement.value) {
      event.stopPropagation();
    }
  }
}