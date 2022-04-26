import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatriculaFormasPagamentoComponent } from './matricula-formas-pagamento.component';

describe('MatriculaFormasPagamentoComponent', () => {
  let component: MatriculaFormasPagamentoComponent;
  let fixture: ComponentFixture<MatriculaFormasPagamentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatriculaFormasPagamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatriculaFormasPagamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
