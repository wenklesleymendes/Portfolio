import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AnexoContasPagarComponent } from './anexo-contas-pagar.component';

describe('AnexoContasPagarComponent', () => {
  let component: AnexoContasPagarComponent;
  let fixture: ComponentFixture<AnexoContasPagarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnexoContasPagarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnexoContasPagarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
