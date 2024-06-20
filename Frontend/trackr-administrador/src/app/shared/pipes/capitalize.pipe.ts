import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'capitalize'
})
export class CapitalizePipe implements PipeTransform {

  transform(value: string | null): string {
    if (value === null) {
      return '';
    }
    return value.charAt(0).toUpperCase() + value.slice(1);
  }

}