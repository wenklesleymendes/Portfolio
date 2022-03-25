import { TestBed } from '@angular/core/testing';

import { MatriculaAlunoService } from './matricula-aluno.service';

describe('MatriculaAlunoService', () => {
  let service: MatriculaAlunoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MatriculaAlunoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
