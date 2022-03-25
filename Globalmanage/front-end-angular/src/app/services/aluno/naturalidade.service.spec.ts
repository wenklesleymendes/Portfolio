import { TestBed } from '@angular/core/testing';

import { NaturalidadeService } from './naturalidade.service';

describe('NaturalidadeService', () => {
  let service: NaturalidadeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NaturalidadeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
