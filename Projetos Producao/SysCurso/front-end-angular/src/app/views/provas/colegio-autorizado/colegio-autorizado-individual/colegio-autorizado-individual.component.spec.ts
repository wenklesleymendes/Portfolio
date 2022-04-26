import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ColegioAutorizadoIndividualComponent } from './colegio-autorizado-individual.component';

describe('ColegioAutorizadoIndividualComponent', () => {
  let component: ColegioAutorizadoIndividualComponent;
  let fixture: ComponentFixture<ColegioAutorizadoIndividualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ColegioAutorizadoIndividualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ColegioAutorizadoIndividualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
