import { createSelector, createFeatureSelector } from '@ngrx/store';
import { Aluno } from './state';

const selectAlunoState = createFeatureSelector<Aluno>('aluno');

export const selectAluno            = createSelector(selectAlunoState, (state: Aluno): any => state);
export const selectCadastroAluno    = createSelector(selectAlunoState, (state: Aluno): any => state?.cadastro);
export const selectMatriculaAluno   = createSelector(selectAlunoState, (state: Aluno): any => state?.matricula);
export const selectImgAluno         = createSelector(selectAlunoState, (state: Aluno): any => state?.img);
export const selectCursoAluno       = createSelector(selectAlunoState, (state: Aluno): any => state?.curso);
