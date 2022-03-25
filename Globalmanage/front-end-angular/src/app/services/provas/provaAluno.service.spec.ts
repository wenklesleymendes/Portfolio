import { TestBed } from '@angular/core/testing';

import { ProvaAlunoService } from './provaAluno.service';

describe('ProvasService', () => {
  let service: ProvaAlunoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProvaAlunoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
