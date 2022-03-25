import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import { alunoReducer } from './reducer';
import { EffectsModule } from '@ngrx/effects';
import { AlunoEffects } from './effects';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    StoreModule.forFeature('aluno', alunoReducer),
    EffectsModule.forRoot([AlunoEffects])
  ]
})
export class AlunoStoreModule { }