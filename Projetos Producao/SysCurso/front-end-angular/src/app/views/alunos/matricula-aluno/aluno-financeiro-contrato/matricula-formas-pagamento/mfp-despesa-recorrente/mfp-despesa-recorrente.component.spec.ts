import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MfpDespesaRecorrenteComponent } from './mfp-despesa-recorrente.component';

describe('MfpDespesaRecorrenteComponent', () => {
  let component: MfpDespesaRecorrenteComponent;
  let fixture: ComponentFixture<MfpDespesaRecorrenteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MfpDespesaRecorrenteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MfpDespesaRecorrenteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
