import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EjaEnccejaComponent } from './eja-encceja.component';

describe('EjaEnccejaComponent', () => {
  let component: EjaEnccejaComponent;
  let fixture: ComponentFixture<EjaEnccejaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EjaEnccejaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EjaEnccejaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
