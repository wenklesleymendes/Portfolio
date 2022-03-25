import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SolicitacoesGeradasComponent } from './solicitacoes-geradas.component';

describe('SolicitacoesGeradasComponent', () => {
  let component: SolicitacoesGeradasComponent;
  let fixture: ComponentFixture<SolicitacoesGeradasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SolicitacoesGeradasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SolicitacoesGeradasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
