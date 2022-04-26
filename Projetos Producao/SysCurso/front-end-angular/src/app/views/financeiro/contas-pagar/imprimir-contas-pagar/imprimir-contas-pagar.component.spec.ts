import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ImprimirContasPagarComponent } from './imprimir-contas-pagar.component';

describe('ImprimirContasPagarComponent', () => {
  let component: ImprimirContasPagarComponent;
  let fixture: ComponentFixture<ImprimirContasPagarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ImprimirContasPagarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ImprimirContasPagarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
