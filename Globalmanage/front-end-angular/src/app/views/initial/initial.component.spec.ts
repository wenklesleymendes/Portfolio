import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FirstPageAlunoComponent } from './initial.component';

describe('FirstPageAlunoComponent', () => {
  let component: FirstPageAlunoComponent;
  let fixture: ComponentFixture<FirstPageAlunoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FirstPageAlunoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FirstPageAlunoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
