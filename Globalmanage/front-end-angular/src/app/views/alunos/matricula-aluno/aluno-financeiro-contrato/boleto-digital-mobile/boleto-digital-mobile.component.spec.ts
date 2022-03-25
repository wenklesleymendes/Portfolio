import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BoletoDigitalMobileComponent } from './boleto-digital-mobile.component';


describe('BoletoDigitalMobileComponent', () => {
  let component: BoletoDigitalMobileComponent;
  let fixture: ComponentFixture<BoletoDigitalMobileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BoletoDigitalMobileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BoletoDigitalMobileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});