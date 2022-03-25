import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RComissoesComponent } from './r-comissoes.component';

describe('RComissoesComponent', () => {
  let component: RComissoesComponent;
  let fixture: ComponentFixture<RComissoesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RComissoesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RComissoesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});