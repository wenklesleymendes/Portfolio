import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RCertificadosComponent } from './r-certificados.component';

describe('RCertificadosComponent', () => {
  let component: RCertificadosComponent;
  let fixture: ComponentFixture<RCertificadosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RCertificadosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RCertificadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});