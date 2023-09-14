import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import { alunoReducer } from './reducer';
import { EffectsModule } from '@ngrx/effects';
import { FinanceiroEffects } from './effects';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    StoreModule.forFeature('financeiro', alunoReducer),
    EffectsModule.forRoot([FinanceiroEffects])
  ]
})
export class FinanceiroStoreModule { }