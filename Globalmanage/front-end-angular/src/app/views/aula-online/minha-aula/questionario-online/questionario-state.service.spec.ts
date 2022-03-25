import { TestBed } from '@angular/core/testing';

import { QuestionarioStateService } from './questionario-state.service';

describe('QuestionarioStateService', () => {
  let service: QuestionarioStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QuestionarioStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
