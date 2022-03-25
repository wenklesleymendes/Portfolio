import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EstoqueIndividualComponent } from './estoque-individual.component';

describe('EstoqueIndividualComponent', () => {
  let component: EstoqueIndividualComponent;
  let fixture: ComponentFixture<EstoqueIndividualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EstoqueIndividualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EstoqueIndividualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
