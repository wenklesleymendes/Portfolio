import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuaseLaComponent } from './quase-la.component';

describe('QuaseLaComponent', () => {
  let component: QuaseLaComponent;
  let fixture: ComponentFixture<QuaseLaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuaseLaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuaseLaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
