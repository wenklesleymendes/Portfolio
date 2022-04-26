import { createReducer, on } from '@ngrx/store';
import { Aluno } from './state';
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

export const initialState: Aluno = null;

export const alunoReducer = createReducer(
    initialState,
    on(alunoActions.deleteAluno, (state) => state = null),

    on(alunoActions.updateCadastroAluno, (state, { payload }) => update(state, 'cadastro', payload)),    
    on(alunoActions.deleteCadastroAluno, (state) => remove(state, 'cadastro')),

    on(alunoActions.updateMatriculaAluno, (state, { payload }) => update(state, 'matricula', payload)),    
    on(alunoActions.deleteMatriculaAluno, (state) => remove(state, 'matricula')),

    on(alunoActions.updateImgAluno, (state, { payload }) => state = { cadastro: state?.cadastro, img: payload }),
    on(alunoActions.deleteImgAluno, (state) => remove(state, 'IMG')),

    on(alunoActions.updateCursoAluno, (state, { payload }) => update(state, 'curso', payload)),
    on(alunoActions.deleteCursoAluno, (state) => remove(state, 'curso')),
);