import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatriculaDetalhesPagamentoComponent } from './matricula-detalhes-pagamento.component';

describe('MatriculaDetalhesPagamentoComponent', () => {
  let component: MatriculaDetalhesPagamentoComponent;
  let fixture: ComponentFixture<MatriculaDetalhesPagamentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatriculaDetalhesPagamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatriculaDetalhesPagamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
