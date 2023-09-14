import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastrarAlunosComponent } from './cadastrar-alunos.component';

describe('CadastrarAlunosComponent', () => {
  let component: CadastrarAlunosComponent;
  let fixture: ComponentFixture<CadastrarAlunosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CadastrarAlunosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CadastrarAlunosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
