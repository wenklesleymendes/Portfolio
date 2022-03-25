import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BoletoAvisoComponent } from './boleto-aviso.component';

describe('BoletoAvisoComponent', () => {
  let component: BoletoAvisoComponent;
  let fixture: ComponentFixture<BoletoAvisoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BoletoAvisoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BoletoAvisoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
