import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompartilheComponent } from './compartilhe.component';

describe('CompartilheComponent', () => {
  let component: CompartilheComponent;
  let fixture: ComponentFixture<CompartilheComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompartilheComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompartilheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});