import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DatePtBrPipe } from './date-pt-br.pipe';
import { SafeHtmlPipe } from './safe-html.pipe';
import { TextLimitPipe } from './text-limit.pipe';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    DatePtBrPipe,
    SafeHtmlPipe,
    TextLimitPipe
  ],
  exports: [
    DatePtBrPipe,
    SafeHtmlPipe,
    TextLimitPipe
  ], 
  providers: [
    DatePtBrPipe,
    SafeHtmlPipe,
    TextLimitPipe
  ]
})
export class PipeModule { }
