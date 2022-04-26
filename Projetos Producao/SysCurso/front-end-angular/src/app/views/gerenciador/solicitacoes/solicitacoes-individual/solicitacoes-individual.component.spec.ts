import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SolicitacoesIndividualComponent } from './solicitacoes-individual.component';

describe('SolicitacoesIndividualComponent', () => {
  let component: SolicitacoesIndividualComponent;
  let fixture: ComponentFixture<SolicitacoesIndividualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SolicitacoesIndividualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SolicitacoesIndividualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
