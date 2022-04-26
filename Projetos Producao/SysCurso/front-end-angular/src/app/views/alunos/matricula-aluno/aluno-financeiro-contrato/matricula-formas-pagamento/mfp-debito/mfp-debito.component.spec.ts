import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MfpDebitoComponent } from './mfp-debito.component';

describe('MfpDebitoComponent', () => {
  let component: MfpDebitoComponent;
  let fixture: ComponentFixture<MfpDebitoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MfpDebitoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MfpDebitoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
