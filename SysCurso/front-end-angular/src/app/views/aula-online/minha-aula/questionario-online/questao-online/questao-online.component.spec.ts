import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestaoOnlineComponent } from './questao-online.component';

describe('QuestaoOnlineComponent', () => {
  let component: QuestaoOnlineComponent;
  let fixture: ComponentFixture<QuestaoOnlineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuestaoOnlineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestaoOnlineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
