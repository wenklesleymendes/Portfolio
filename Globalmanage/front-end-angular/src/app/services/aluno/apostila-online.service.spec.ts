import { TestBed } from '@angular/core/testing';

import { ApostilaOnlineService } from './apostila-online.service';

describe('ApostilaOnlineService', () => {
  let service: ApostilaOnlineService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApostilaOnlineService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
