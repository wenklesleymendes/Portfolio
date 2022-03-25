import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PainelTicketComponent } from './painel-ticket.component';

describe('PainelTicketComponent', () => {
  let component: PainelTicketComponent;
  let fixture: ComponentFixture<PainelTicketComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PainelTicketComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PainelTicketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
