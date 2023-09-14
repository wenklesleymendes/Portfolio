import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlunoCursoTurmaSelectComponent } from './aluno-curso-turma-select.component';

describe('AlunoCursoTurmaSelectComponent', () => {
  let component: AlunoCursoTurmaSelectComponent;
  let fixture: ComponentFixture<AlunoCursoTurmaSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlunoCursoTurmaSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlunoCursoTurmaSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
