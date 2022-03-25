import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatriculaCancelamentoComponent } from './matricula-cancelamento.component';

describe('MatriculaCancelamentoComponent', () => {
  let component: MatriculaCancelamentoComponent;
  let fixture: ComponentFixture<MatriculaCancelamentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatriculaCancelamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatriculaCancelamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
