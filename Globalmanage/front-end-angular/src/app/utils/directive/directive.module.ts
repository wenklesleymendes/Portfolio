import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TitlecaseDirective } from './titlecase.directive';
import { UppercaseDirective } from './uppercase.directive';
import { LowwercaseDirective } from './lowwercase.directive';



@NgModule({
  declarations: [
    TitlecaseDirective,
    UppercaseDirective,
    LowwercaseDirective
  ],
  imports: [
    CommonModule
  ],
  exports: [
    TitlecaseDirective,
    UppercaseDirective,
    LowwercaseDirective
  ]
})
export class DirectiveModule { }
