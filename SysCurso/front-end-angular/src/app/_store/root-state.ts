import { AlunoStoreState } from './aluno-store';
import { FinanceiroStoreState } from './financeiro-store';

export interface RootState {
  alunoStoreState: AlunoStoreState.Aluno,
  financeiroStoreState: FinanceiroStoreState.Financeiro
}