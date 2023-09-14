import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MppDetalhePagamentoComponent } from './mpp-detalhe-pagamento.component';

describe('MppDetalhePagamentoComponent', () => {
  let component: MppDetalhePagamentoComponent;
  let fixture: ComponentFixture<MppDetalhePagamentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MppDetalhePagamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MppDetalhePagamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
