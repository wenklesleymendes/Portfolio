import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RFinanceiroComponent } from './r-financeiro.component';

describe('RFinanceiroComponent', () => {
  let component: RFinanceiroComponent;
  let fixture: ComponentFixture<RFinanceiroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RFinanceiroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RFinanceiroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});