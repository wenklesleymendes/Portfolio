import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContasPagarDetalheComponent } from './contas-pagar-detalhe.component';

describe('ContasPagarDetalheComponent', () => {
  let component: ContasPagarDetalheComponent;
  let fixture: ComponentFixture<ContasPagarDetalheComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContasPagarDetalheComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContasPagarDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
