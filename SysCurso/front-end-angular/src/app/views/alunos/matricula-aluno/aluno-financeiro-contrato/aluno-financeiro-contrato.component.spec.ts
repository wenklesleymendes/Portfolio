import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlunoFinanceiroContratoComponent } from './aluno-financeiro-contrato.component';

describe('AlunoFinanceiroContratoComponent', () => {
  let component: AlunoFinanceiroContratoComponent;
  let fixture: ComponentFixture<AlunoFinanceiroContratoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlunoFinanceiroContratoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlunoFinanceiroContratoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
