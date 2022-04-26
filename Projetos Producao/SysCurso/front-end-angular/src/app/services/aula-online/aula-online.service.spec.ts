import { TestBed } from '@angular/core/testing';

import { AulaOnlineService } from './aula-online.service';

describe('AulaOnlineService', () => {
  let service: AulaOnlineService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AulaOnlineService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
