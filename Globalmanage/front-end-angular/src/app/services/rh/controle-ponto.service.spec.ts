import { TestBed } from '@angular/core/testing';

import { ControlePontoService } from './controle-ponto.service';

describe('ControlePontoService', () => {
  let service: ControlePontoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ControlePontoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
