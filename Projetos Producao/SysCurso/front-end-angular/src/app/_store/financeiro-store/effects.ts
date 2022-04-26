import { Injectable } from '@angular/core';
import { Actions } from '@ngrx/effects';

@Injectable()
export class FinanceiroEffects {

  constructor(
    private actions$: Actions
  ) {}
}