import { TestBed } from '@angular/core/testing';

import { MatriculaCancelamentoService } from './matricula-cancelamento.service';

describe('MatriculaCancelamentoService', () => {
  let service: MatriculaCancelamentoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MatriculaCancelamentoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
