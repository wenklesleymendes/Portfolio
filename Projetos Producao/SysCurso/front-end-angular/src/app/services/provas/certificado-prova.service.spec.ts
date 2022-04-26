/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CertificadoProvaService } from './certificado-prova.service';

describe('Service: CertificadoProva', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CertificadoProvaService]
    });
  });

  it('should ...', inject([CertificadoProvaService], (service: CertificadoProvaService) => {
    expect(service).toBeTruthy();
  }));
});
