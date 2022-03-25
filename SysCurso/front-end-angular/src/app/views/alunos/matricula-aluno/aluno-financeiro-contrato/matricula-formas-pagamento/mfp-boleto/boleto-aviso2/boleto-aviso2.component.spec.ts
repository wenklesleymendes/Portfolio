import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BoletoAviso2Component } from './boleto-aviso2.component';

describe('BoletoAviso2Component', () => {
  let component: BoletoAviso2Component;
  let fixture: ComponentFixture<BoletoAviso2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BoletoAviso2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BoletoAviso2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
