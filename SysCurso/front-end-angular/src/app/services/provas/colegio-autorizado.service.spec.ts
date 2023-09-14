import { TestBed } from '@angular/core/testing';

import { ColegioAutorizadoService } from './colegio-autorizado.service';

describe('ColegioAutorizadoService', () => {
  let service: ColegioAutorizadoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ColegioAutorizadoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
