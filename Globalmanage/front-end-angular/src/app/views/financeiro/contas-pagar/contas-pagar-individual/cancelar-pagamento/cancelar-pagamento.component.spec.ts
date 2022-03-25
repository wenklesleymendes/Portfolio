import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CancelarPagamentoComponent } from './cancelar-pagamento.component';

describe('CancelarPagamentoComponent', () => {
  let component: CancelarPagamentoComponent;
  let fixture: ComponentFixture<CancelarPagamentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CancelarPagamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CancelarPagamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
