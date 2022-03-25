import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FolhaPagamentoReciboComponent } from './folha-pagamento-recibo.component';

describe('FolhaPagamentoReciboComponent', () => {
  let component: FolhaPagamentoReciboComponent;
  let fixture: ComponentFixture<FolhaPagamentoReciboComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FolhaPagamentoReciboComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FolhaPagamentoReciboComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
