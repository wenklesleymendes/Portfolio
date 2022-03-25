import { createSelector, createFeatureSelector } from '@ngrx/store';
import { Financeiro } from './state';

const selectFinanceiroState = createFeatureSelector<Financeiro>('financeiro');

export const selectFinanceiro  = createSelector(selectFinanceiroState, (state: Financeiro): any => state);
export const selectPlanoPagamentoFinanceiro = createSelector(selectFinanceiroState, (state: Financeiro): any => state?.planoPagamento);
export const selectDadosSelecionadosFinanceiro = createSelector(selectFinanceiroState, (state: Financeiro): any => state?.dadosSelecionados);
export const selectCampanhaFinanceiro = createSelector(selectFinanceiroState, (state: Financeiro): any => state?.campanha);
export const selectFinanceiroCadastrado= createSelector(selectFinanceiroState, (state: Financeiro): any => state?.financeiroCadastrado);
export const selectFinanceiroComprovante= createSelector(selectFinanceiroState, (state: Financeiro): any => state?.comprovante);
export const selectPlanoPagamentoIndividualFinanceiro = createSelector(selectFinanceiroState, (state: Financeiro, props): any => { 
    if(state?.planoPagamento?.length > 0) {
        return state.planoPagamento.find(elem => elem?.id === props.planoPagamentoId);
    }
});