import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AfiliacaoComponent } from './afiliacao.component';

describe('AfiliacaoComponent', () => {
  let component: AfiliacaoComponent;
  let fixture: ComponentFixture<AfiliacaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AfiliacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AfiliacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
