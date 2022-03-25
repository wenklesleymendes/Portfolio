import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatriculaCancelamentoAutorizacaoComponent } from './matricula-cancelamento-autorizacao.component';

describe('MatriculaCancelamentoAutorizacaoComponent', () => {
  let component: MatriculaCancelamentoAutorizacaoComponent;
  let fixture: ComponentFixture<MatriculaCancelamentoAutorizacaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatriculaCancelamentoAutorizacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatriculaCancelamentoAutorizacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
