import { TestBed } from '@angular/core/testing';

import { ProvasService } from './provas.service';

describe('ProvasService', () => {
  let service: ProvasService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProvasService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
