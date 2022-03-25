import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PerguntasOnlineComponent } from './perguntas-online.component';

describe('PerguntasOnlineComponent', () => {
  let component: PerguntasOnlineComponent;
  let fixture: ComponentFixture<PerguntasOnlineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PerguntasOnlineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PerguntasOnlineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
