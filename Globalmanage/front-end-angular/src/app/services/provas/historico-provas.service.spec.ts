import { TestBed } from '@angular/core/testing';

import { HistoricoProvasService } from './historico-provas.service';

describe('HistoricoProvasService', () => {
  let service: HistoricoProvasService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HistoricoProvasService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});