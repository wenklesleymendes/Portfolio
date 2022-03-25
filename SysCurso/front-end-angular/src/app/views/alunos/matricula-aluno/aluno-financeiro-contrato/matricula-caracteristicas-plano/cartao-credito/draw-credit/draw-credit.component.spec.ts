import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DrawCreditComponent } from './draw-credit.component';

describe('DrawCreditComponent', () => {
  let component: DrawCreditComponent;
  let fixture: ComponentFixture<DrawCreditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DrawCreditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DrawCreditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
