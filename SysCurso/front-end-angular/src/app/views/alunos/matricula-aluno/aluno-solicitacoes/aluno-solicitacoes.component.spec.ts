import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlunoSolicitacoesComponent } from './aluno-solicitacoes.component';

describe('AlunoSolicitacoesComponent', () => {
  let component: AlunoSolicitacoesComponent;
  let fixture: ComponentFixture<AlunoSolicitacoesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlunoSolicitacoesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlunoSolicitacoesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
