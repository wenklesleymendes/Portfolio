import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlunoTicketNovoComponent } from './aluno-ticket-novo.component';

describe('AlunoTicketNovoComponent', () => {
  let component: AlunoTicketNovoComponent;
  let fixture: ComponentFixture<AlunoTicketNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlunoTicketNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlunoTicketNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
