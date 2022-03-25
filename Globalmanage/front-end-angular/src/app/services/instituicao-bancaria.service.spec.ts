import { TestBed } from '@angular/core/testing';

import { InstituicaoBancariaService } from './instituicao-bancaria.service';

describe('InstituicaoBancariaService', () => {
  let service: InstituicaoBancariaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InstituicaoBancariaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
