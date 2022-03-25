import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoricoAgendadosComponent } from './historico-agendados.component';

describe('HistoricoAgendadosComponent', () => {
  let component: HistoricoAgendadosComponent;
  let fixture: ComponentFixture<HistoricoAgendadosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HistoricoAgendadosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HistoricoAgendadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
