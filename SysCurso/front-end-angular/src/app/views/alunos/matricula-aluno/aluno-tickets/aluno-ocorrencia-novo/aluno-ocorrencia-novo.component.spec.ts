import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlunoOcorrenciaNovoComponent } from './aluno-ocorrencia-novo.component';

describe('AlunoOcorrenciaNovoComponent', () => {
  let component: AlunoOcorrenciaNovoComponent;
  let fixture: ComponentFixture<AlunoOcorrenciaNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlunoOcorrenciaNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlunoOcorrenciaNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
