import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RProvasComponent } from './r-provas.component';

describe('RProvasComponent', () => {
  let component: RProvasComponent;
  let fixture: ComponentFixture<RProvasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RProvasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RProvasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});