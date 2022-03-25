import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'datePtBr'
})
export class DatePtBrPipe implements PipeTransform {

  transform(value: any, ...args: any[]): any {
    if (!value || value == '') return null;

    let date: Date    = new Date(value);

    let day: string   = date.getUTCDate().toString().padStart(2, '0');
    // January is considered 0 in javascript
    let month: string = (date.getUTCMonth() + 1).toString().padStart(2, '0');
    let year:string   = date.getUTCFullYear().toString();

    return `${day}/${month}/${year}`;
  }
}
