import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MppReciboComponent } from './mpp-recibo.component';

describe('MppReciboComponent', () => {
  let component: MppReciboComponent;
  let fixture: ComponentFixture<MppReciboComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MppReciboComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MppReciboComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
