import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FolhaPagamentoHistoricoComponent } from './folha-pagamento-historico.component';

describe('FolhaPagamentoHistoricoComponent', () => {
  let component: FolhaPagamentoHistoricoComponent;
  let fixture: ComponentFixture<FolhaPagamentoHistoricoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FolhaPagamentoHistoricoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FolhaPagamentoHistoricoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
