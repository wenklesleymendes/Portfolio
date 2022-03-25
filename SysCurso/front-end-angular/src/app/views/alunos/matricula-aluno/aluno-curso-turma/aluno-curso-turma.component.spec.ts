import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlunoCursoTurmaComponent } from './aluno-curso-turma.component';

describe('AlunoCursoTurmaComponent', () => {
  let component: AlunoCursoTurmaComponent;
  let fixture: ComponentFixture<AlunoCursoTurmaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlunoCursoTurmaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlunoCursoTurmaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
