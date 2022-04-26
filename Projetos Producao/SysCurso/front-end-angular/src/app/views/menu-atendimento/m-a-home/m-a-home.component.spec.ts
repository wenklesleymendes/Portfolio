import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MAHomeComponent } from './m-a-home.component';

describe('MAHomeComponent', () => {
  let component: MAHomeComponent;
  let fixture: ComponentFixture<MAHomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MAHomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MAHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
