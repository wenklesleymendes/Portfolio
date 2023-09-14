import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlunoProvaCertificadoEmissaoComponent } from './aluno-prova-certificado-emissao.component';

describe('AlunoProvaCertificadoEmissaoComponent', () => {
  let component: AlunoProvaCertificadoEmissaoComponent;
  let fixture: ComponentFixture<AlunoProvaCertificadoEmissaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlunoProvaCertificadoEmissaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlunoProvaCertificadoEmissaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
