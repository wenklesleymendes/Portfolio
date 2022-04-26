import { TestBed } from '@angular/core/testing';

import { AssuntoTicketService } from './assunto-ticket.service';

describe('AssuntoTicketService', () => {
  let service: AssuntoTicketService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AssuntoTicketService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
