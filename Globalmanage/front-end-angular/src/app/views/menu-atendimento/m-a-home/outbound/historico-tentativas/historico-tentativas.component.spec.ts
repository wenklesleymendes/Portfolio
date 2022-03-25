import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoricoTentativasComponent } from './historico-tentativas.component';

describe('HistoricoTentativasComponent', () => {
  let component: HistoricoTentativasComponent;
  let fixture: ComponentFixture<HistoricoTentativasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HistoricoTentativasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HistoricoTentativasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
