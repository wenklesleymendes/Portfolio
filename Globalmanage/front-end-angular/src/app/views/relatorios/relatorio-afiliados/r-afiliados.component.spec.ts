import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RAfiliadosComponent } from './r-afiliados.component';

describe('RAfiliadosComponent', () => {
  let component: RAfiliadosComponent;
  let fixture: ComponentFixture<RAfiliadosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RAfiliadosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RAfiliadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});