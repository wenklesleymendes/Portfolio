/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { OutboundServiceService } from './OutboundService.service';

describe('Service: OutboundService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [OutboundServiceService]
    });
  });

  it('should ...', inject([OutboundServiceService], (service: OutboundServiceService) => {
    expect(service).toBeTruthy();
  }));
});
