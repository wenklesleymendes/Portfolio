import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MsgRecusarComponent } from './msg-recusar.component';

describe('MsgRecusarComponent', () => {
  let component: MsgRecusarComponent;
  let fixture: ComponentFixture<MsgRecusarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MsgRecusarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MsgRecusarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
