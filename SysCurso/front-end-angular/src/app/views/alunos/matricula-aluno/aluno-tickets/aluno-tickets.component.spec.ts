import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlunoTicketsComponent } from './aluno-tickets.component';

describe('AlunoTicketsComponent', () => {
  let component: AlunoTicketsComponent;
  let fixture: ComponentFixture<AlunoTicketsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlunoTicketsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlunoTicketsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
