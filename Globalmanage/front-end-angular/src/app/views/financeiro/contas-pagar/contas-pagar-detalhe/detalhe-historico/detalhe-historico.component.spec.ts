import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalheHistoricoComponent } from './detalhe-historico.component';

describe('DetalheHistoricoComponent', () => {
  let component: DetalheHistoricoComponent;
  let fixture: ComponentFixture<DetalheHistoricoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetalheHistoricoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetalheHistoricoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
