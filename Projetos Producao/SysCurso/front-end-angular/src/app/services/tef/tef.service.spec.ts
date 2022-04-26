import { TestBed } from '@angular/core/testing';

import { TefService } from './tef.service';

describe('TefService', () => {
  let service: TefService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TefService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
