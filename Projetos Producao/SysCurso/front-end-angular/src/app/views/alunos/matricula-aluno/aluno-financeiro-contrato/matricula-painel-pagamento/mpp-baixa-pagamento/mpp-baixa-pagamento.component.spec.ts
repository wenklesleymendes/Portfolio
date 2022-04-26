import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MppBaixaPagamentoComponent } from './mpp-baixa-pagamento.component';

describe('MppBaixaPagamentoComponent', () => {
  let component: MppBaixaPagamentoComponent;
  let fixture: ComponentFixture<MppBaixaPagamentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MppBaixaPagamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MppBaixaPagamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
