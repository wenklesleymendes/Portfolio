import { TestBed } from '@angular/core/testing';

import { ContratoAlunoService } from './contrato-aluno.service';

describe('ContratoAlunoService', () => {
  let service: ContratoAlunoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ContratoAlunoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
