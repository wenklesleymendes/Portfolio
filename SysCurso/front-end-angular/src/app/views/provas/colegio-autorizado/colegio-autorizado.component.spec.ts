import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ColegioAutorizadoComponent } from './colegio-autorizado.component';

describe('ColegioAutorizadoComponent', () => {
  let component: ColegioAutorizadoComponent;
  let fixture: ComponentFixture<ColegioAutorizadoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ColegioAutorizadoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ColegioAutorizadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
