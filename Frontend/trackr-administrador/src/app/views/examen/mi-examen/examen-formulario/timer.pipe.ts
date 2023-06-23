import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'timer'})
export class TimerPipe implements PipeTransform {
  transform(segundos: number): string {
    return this.toTimer(segundos);
  }

  public toTimer(segundosRestantes: number): string {
    const minutos = Math.floor(segundosRestantes / 60);
    const segundos = segundosRestantes % 60;

    const doubleDigit = segundos < 10 ? '0' : '';

    return `${minutos}:${doubleDigit}${segundos}`;
  }
}
