import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RDisparosRealizadosComponent } from './r-disparos-realizados.component';

describe('RDisparosRealizadosComponent', () => {
  let component: RDisparosRealizadosComponent;
  let fixture: ComponentFixture<RDisparosRealizadosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RDisparosRealizadosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RDisparosRealizadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});