import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatriculaAlunoComponent } from './matricula-aluno.component';

describe('MatriculaAlunoComponent', () => {
  let component: MatriculaAlunoComponent;
  let fixture: ComponentFixture<MatriculaAlunoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatriculaAlunoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatriculaAlunoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
