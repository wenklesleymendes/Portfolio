import { TestBed } from '@angular/core/testing';

import { TransporteProvaService } from './transporteProva.service';

describe('TransporteProvaService', () => {
  let service: TransporteProvaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TransporteProvaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
