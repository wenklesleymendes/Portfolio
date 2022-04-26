import { createAction, props } from '@ngrx/store';

export const deleteAluno = createAction('[ALUNO] Delete');

export const updateCadastroAluno = createAction('[ALUNO CADASTRO] Update', props<{ payload: any }>());
export const deleteCadastroAluno = createAction('[ALUNO CADASTRO] Delete');

export const updateMatriculaAluno = createAction('[ALUNO MATRICULA] Update', props<{ payload: any }>());
export const deleteMatriculaAluno = createAction('[ALUNO MATRICULA] Delete');

export const updateImgAluno = createAction('[ALUNO IMG] Update', props<{ payload: { foto: string, extensao } }>());
export const deleteImgAluno = createAction('[ALUNO IMG] Delete');

export const updateCursoAluno = createAction('[ALUNO CURSO] Update', props<{ payload: any }>());
export const deleteCursoAluno = createAction('[ALUNO CURSO] Delete');
