import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmarDadosComponent } from './confirmar-dados.component';

describe('ConfirmarDadosComponent', () => {
  let component: ConfirmarDadosComponent;
  let fixture: ComponentFixture<ConfirmarDadosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfirmarDadosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmarDadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
