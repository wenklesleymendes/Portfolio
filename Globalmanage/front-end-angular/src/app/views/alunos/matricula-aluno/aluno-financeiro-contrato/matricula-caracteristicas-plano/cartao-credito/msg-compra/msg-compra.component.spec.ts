import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MsgCompraComponent } from './msg-compra.component';

describe('MsgCompraComponent', () => {
  let component: MsgCompraComponent;
  let fixture: ComponentFixture<MsgCompraComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MsgCompraComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MsgCompraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
