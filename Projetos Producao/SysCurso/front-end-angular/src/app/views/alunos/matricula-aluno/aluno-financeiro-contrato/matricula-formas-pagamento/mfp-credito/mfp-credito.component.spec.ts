import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MfpCreditoComponent } from './mfp-credito.component';

describe('MfpCreditoComponent', () => {
  let component: MfpCreditoComponent;
  let fixture: ComponentFixture<MfpCreditoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MfpCreditoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MfpCreditoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
