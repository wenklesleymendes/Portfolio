import { TestBed } from '@angular/core/testing';

import { ContasPagarService } from './contas-pagar.service';

describe('ContasPagarService', () => {
  let service: ContasPagarService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ContasPagarService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
