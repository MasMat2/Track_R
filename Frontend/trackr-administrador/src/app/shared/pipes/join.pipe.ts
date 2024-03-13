import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: 'join'
})
export class JoinPipe implements PipeTransform {

  constructor() { }

  transform(input:Array<any>, sep = ','): string {
    return input.join(sep);
  }
}
