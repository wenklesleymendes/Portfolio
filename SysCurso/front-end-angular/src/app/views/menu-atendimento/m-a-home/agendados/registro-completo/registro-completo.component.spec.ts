import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistroCompletoComponent } from './registro-completo.component';

describe('RegistroCompletoComponent', () => {
  let component: RegistroCompletoComponent;
  let fixture: ComponentFixture<RegistroCompletoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistroCompletoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistroCompletoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
