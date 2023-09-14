import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoricoProvaComponent } from './historico-prova.component';

describe('HistoricoProvaComponent', () => {
  let component: HistoricoProvaComponent;
  let fixture: ComponentFixture<HistoricoProvaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HistoricoProvaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HistoricoProvaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
