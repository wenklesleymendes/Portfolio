import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketIndividualComponent } from './ticket-individual.component';

describe('TicketIndividualComponent', () => {
  let component: TicketIndividualComponent;
  let fixture: ComponentFixture<TicketIndividualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TicketIndividualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TicketIndividualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
