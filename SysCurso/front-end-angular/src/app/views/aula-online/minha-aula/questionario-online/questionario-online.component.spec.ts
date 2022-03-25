import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionarioOnlineComponent } from './questionario-online.component';

describe('QuestionarioOnlineComponent', () => {
  let component: QuestionarioOnlineComponent;
  let fixture: ComponentFixture<QuestionarioOnlineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuestionarioOnlineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestionarioOnlineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
