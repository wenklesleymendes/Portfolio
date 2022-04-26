import { createAction, props } from '@ngrx/store';

export const deleteFinanceiro = createAction('[FINANCEIRO] Delete');

export const updatePlanoPagamento = createAction('[FINANCEIRO PLANO PAGAMENTO] Update', props<{ payload: any }>());
export const deletePlanoPagamento = createAction('[FINANCEIRO PLANO PAGAMENTO] Delete');

export const updateDadosSelecionados = createAction('[FINANCEIRO DADOS SELECIONADOS PLANO PAGAMENTO] Update', props<{ payload: any }>());
export const deleteDadosSelecionados = createAction('[FINANCEIRO DADOS SELECIONADOS PLANO PAGAMENTO] Delete');

export const updateCampanha = createAction('[FINANCEIRO CAMPANHA PLANO PAGAMENTO] Update', props<{ payload: any }>());
export const deleteCampanha = createAction('[FINANCEIRO CAMPANHA PLANO PAGAMENTO] Delete');

export const updateFinanceiroCadastrado = createAction('[FINANCEIRO CADASTRADO] Update', props<{ payload: any }>());
export const deleteFinanceiroCadastrado = createAction('[FINANCEIRO CADASTRADO] Delete');

export const updateFinanceiroComprovante = createAction('[FINANCEIRO COMPROVANTE] Update', props<{ payload: any }>());
export const deleteFinanceiroComprovante = createAction('[FINANCEIRO COMPROVANTE] Delete');