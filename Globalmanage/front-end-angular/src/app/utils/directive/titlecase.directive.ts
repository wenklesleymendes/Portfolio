import { A,  Z,} from '@angular/cdk/keycodes';
import { stringify } from '@angular/compiler/src/util';
import {  Directive,  ElementRef,  forwardRef,  HostListener,  OnInit,  Renderer2,  Self,} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Directive({
  selector: '[appTitlecase]',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TitlecaseDirective),
      multi: true,
    },
  ]
})
export class TitlecaseDirective implements ControlValueAccessor {

  /** implements ControlValueAccessorInterface */
  _onChange: (_: any) => void;

  /** implements ControlValueAccessorInterface */
  _touched: () => void;

  constructor( @Self() private _el: ElementRef, private _renderer: Renderer2) { }

  /** Trata as teclas */
  @HostListener('keyup', ['$event'])
  onKeyDown(evt: KeyboardEvent) {
    // const key = evt.key;
    // console.log(evt)
    // console.log(this._el.nativeElement.value)

     var value: string= "";

      var listaTexto: string[] = this._el.nativeElement.value.replaceAll(" ", " |").split("|");

      for (let index = 0; index < listaTexto.length; index++) {
          const element = listaTexto[index];

          // console.log(element);
          value += element.substring(0,1).toUpperCase();

          if(element.length > 1)
            value += element.substring(1).toLowerCase();

            if(index > (listaTexto.length -1))
            value += " ";
      }

      this._renderer.setProperty(this._el.nativeElement, 'value', value);
      this._onChange(value);
      evt.preventDefault();

  }

  @HostListener('blur', ['$event'])
  onBlur() {
    this._touched();
  }

  /** Implementation for ControlValueAccessor interface */
  writeValue(value: any): void {
    this._renderer.setProperty(this._el.nativeElement, 'value', value);
  }

  /** Implementation for ControlValueAccessor interface */
  registerOnChange(fn: (_: any) => void): void {
    this._onChange = fn;
  }

  /** Implementation for ControlValueAccessor interface */
  registerOnTouched(fn: () => void): void {
    this._touched = fn;
  }

  /** Implementation for ControlValueAccessor interface */
  setDisabledState(isDisabled: boolean): void {
    this._renderer.setProperty(this._el.nativeElement, 'disabled', isDisabled);
  }

}
