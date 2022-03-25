import { TestBed } from '@angular/core/testing';

import { VideoAulaService } from './video-aula.service';

describe('VideoAulaService', () => {
  let service: VideoAulaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VideoAulaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
