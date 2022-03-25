import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatriculaEfetuaCancelamentoComponent } from './matricula-efetua-cancelamento.component';

describe('MatriculaEfetuaCancelamentoComponent', () => {
  let component: MatriculaEfetuaCancelamentoComponent;
  let fixture: ComponentFixture<MatriculaEfetuaCancelamentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatriculaEfetuaCancelamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatriculaEfetuaCancelamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
