import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContasPagarIndividualComponent } from './contas-pagar-individual.component';

describe('ContasPagarIndividualComponent', () => {
  let component: ContasPagarIndividualComponent;
  let fixture: ComponentFixture<ContasPagarIndividualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContasPagarIndividualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContasPagarIndividualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
