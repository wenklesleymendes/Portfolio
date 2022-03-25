import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DinheiroNoBolsoComponent } from './dinheiro-no-bolso.component';

describe('DinheiroNoBolsoComponent', () => {
  let component: DinheiroNoBolsoComponent;
  let fixture: ComponentFixture<DinheiroNoBolsoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DinheiroNoBolsoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DinheiroNoBolsoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});