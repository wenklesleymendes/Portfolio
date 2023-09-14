import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FolhaPagamentoDetalheComponent } from './folha-pagamento-detalhe.component';

describe('FolhaPagamentoDetalheComponent', () => {
  let component: FolhaPagamentoDetalheComponent;
  let fixture: ComponentFixture<FolhaPagamentoDetalheComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FolhaPagamentoDetalheComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FolhaPagamentoDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
