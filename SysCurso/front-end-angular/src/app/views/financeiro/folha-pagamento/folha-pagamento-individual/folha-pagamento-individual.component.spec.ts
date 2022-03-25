import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FolhaPagamentoIndividualComponent } from './folha-pagamento-individual.component';

describe('FolhaPagamentoIndividualComponent', () => {
  let component: FolhaPagamentoIndividualComponent;
  let fixture: ComponentFixture<FolhaPagamentoIndividualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FolhaPagamentoIndividualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FolhaPagamentoIndividualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
