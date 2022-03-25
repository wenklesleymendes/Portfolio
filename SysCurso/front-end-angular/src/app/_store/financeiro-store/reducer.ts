import { createReducer, on } from '@ngrx/store';
import { Financeiro } from './state';
import * as alunoActions from './actions';

const update = (state: any, key: string, payload: any): any  => {
    let found: boolean = false;
    const newState = { ...state };
    for(let item in state) {
        if(item === key) {
            newState[item] = payload;
            found = true;
        }
    }
    if(!found) newState[key] = payload;
    return newState;
}

const remove = (state: any, key: string): any  => {
    let found: boolean = false;
    const newState = { ...state };
    for(let item in state) {
        if(item === key) {
            newState[item] = null;
            found = true;
        }
    }
    return newState;
}

export const initialState: Financeiro = null;

export const alunoReducer = createReducer(
    initialState,
    on(alunoActions.deleteFinanceiro, (state) => state = null),

    on(alunoActions.updatePlanoPagamento, (state, { payload }) => update(state, 'planoPagamento', payload)),
    on(alunoActions.deletePlanoPagamento, (state) => remove(state, 'planoPagamento')),

    on(alunoActions.updateDadosSelecionados, (state, { payload }) => update(state, 'dadosSelecionados', payload)),
    on(alunoActions.deleteDadosSelecionados, (state) => remove(state, 'dadosSelecionados')),

    on(alunoActions.updateCampanha, (state, { payload }) => update(state, 'campanha', payload)),
    on(alunoActions.deleteCampanha, (state) => remove(state, 'campanha')),

    on(alunoActions.updateFinanceiroCadastrado, (state, { payload }) => update(state, 'financeiroCadastrado', payload)),
    on(alunoActions.deleteFinanceiroCadastrado, (state) => remove(state, 'financeiroCadastrado')),

    on(alunoActions.updateFinanceiroComprovante, (state, { payload }) => update(state, 'comprovante', payload)),
    on(alunoActions.deleteFinanceiroComprovante, (state) => remove(state, 'comprovante')),
);