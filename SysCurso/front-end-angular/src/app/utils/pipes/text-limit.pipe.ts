import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'textLimit'
})
export class TextLimitPipe implements PipeTransform {

  transform(value: string, ...args: number[]): unknown {
    if (!value || value == '') return value;
    return value.length > args[0] ? `${value.slice(0, args[0])}...` : value.slice(0, args[0]);;
  }

}
