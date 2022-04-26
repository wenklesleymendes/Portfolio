import { TestBed } from '@angular/core/testing';

import { FolhaPagamentoService } from './folha-pagamento.service';

describe('FolhaPagamentoService', () => {
  let service: FolhaPagamentoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FolhaPagamentoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
