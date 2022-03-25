import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MfpBoletoComponent } from './mfp-boleto.component';

describe('MfpBoletoComponent', () => {
  let component: MfpBoletoComponent;
  let fixture: ComponentFixture<MfpBoletoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MfpBoletoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MfpBoletoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
