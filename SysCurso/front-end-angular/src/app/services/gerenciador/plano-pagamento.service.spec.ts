import { TestBed } from '@angular/core/testing';

import { PlanoPagamentoService } from './plano-pagamento.service';

describe('PlanoPagamentoService', () => {
  let service: PlanoPagamentoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PlanoPagamentoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
