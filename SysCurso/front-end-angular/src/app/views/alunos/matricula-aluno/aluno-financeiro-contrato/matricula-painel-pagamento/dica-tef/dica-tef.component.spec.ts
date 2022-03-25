import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DicaTefComponent } from './dica-tef.component';

describe('DicaTefComponent', () => {
  let component: DicaTefComponent;
  let fixture: ComponentFixture<DicaTefComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DicaTefComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DicaTefComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
