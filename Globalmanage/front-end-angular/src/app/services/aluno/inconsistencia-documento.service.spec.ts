import { TestBed } from '@angular/core/testing';

import { InconsistenciaDocumentoService } from './inconsistencia-documento.service';

describe('InconsistenciaDocumentoService', () => {
  let service: InconsistenciaDocumentoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InconsistenciaDocumentoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
