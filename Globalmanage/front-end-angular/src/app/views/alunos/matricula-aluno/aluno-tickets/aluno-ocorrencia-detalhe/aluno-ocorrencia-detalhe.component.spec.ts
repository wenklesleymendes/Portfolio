import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlunoOcorrenciaDetalheComponent } from './aluno-ocorrencia-detalhe.component';

describe('AlunoOcorrenciaDetalheComponent', () => {
  let component: AlunoOcorrenciaDetalheComponent;
  let fixture: ComponentFixture<AlunoOcorrenciaDetalheComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlunoOcorrenciaDetalheComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlunoOcorrenciaDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
