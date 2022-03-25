import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RCancelamentosComponent } from './r-cancelamentos.component';

describe('RCancelamentosComponent', () => {
  let component: RCancelamentosComponent;
  let fixture: ComponentFixture<RCancelamentosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RCancelamentosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RCancelamentosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});