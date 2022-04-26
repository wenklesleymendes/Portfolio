import { TestBed } from '@angular/core/testing';

import { AlunoFinanceiroService } from './aluno-financeiro.service';

describe('AlunoFinanceiroService', () => {
  let service: AlunoFinanceiroService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AlunoFinanceiroService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
