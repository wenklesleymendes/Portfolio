import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CriarAgendaIndividualComponent } from './criar-agenda-individual.component';

describe('CriarAgendaIndividualComponent', () => {
  let component: CriarAgendaIndividualComponent;
  let fixture: ComponentFixture<CriarAgendaIndividualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CriarAgendaIndividualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CriarAgendaIndividualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
