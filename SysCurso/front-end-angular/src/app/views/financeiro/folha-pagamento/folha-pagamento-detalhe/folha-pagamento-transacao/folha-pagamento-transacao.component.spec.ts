import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FolhaPagamentoTransacaoComponent } from './folha-pagamento-transacao.component';

describe('FolhaPagamentoTransacaoComponent', () => {
  let component: FolhaPagamentoTransacaoComponent;
  let fixture: ComponentFixture<FolhaPagamentoTransacaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FolhaPagamentoTransacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FolhaPagamentoTransacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
