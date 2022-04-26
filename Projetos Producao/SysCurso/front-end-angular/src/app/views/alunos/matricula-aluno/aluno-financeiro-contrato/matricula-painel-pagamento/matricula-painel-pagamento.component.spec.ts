import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatriculaPainelPagamentoComponent } from './matricula-painel-pagamento.component';

describe('MatriculaPainelPagamentoComponent', () => {
  let component: MatriculaPainelPagamentoComponent;
  let fixture: ComponentFixture<MatriculaPainelPagamentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatriculaPainelPagamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatriculaPainelPagamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
