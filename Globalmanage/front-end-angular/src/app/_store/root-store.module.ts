import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { AlunoStoreModule } from './aluno-store/aluno-store.module';
import { FinanceiroStoreModule } from './financeiro-store/financeiro-store.module';

@NgModule({
  imports: [
    CommonModule,
    AlunoStoreModule,
    FinanceiroStoreModule,
    StoreModule.forRoot({}),
    StoreDevtoolsModule.instrument({
      maxAge: 25
    }),
  ],
  declarations: []
})
export class RootStoreModule {}
