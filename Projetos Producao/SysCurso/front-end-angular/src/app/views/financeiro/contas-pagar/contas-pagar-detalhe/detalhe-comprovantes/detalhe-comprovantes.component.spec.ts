import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalheComprovantesComponent } from './detalhe-comprovantes.component';

describe('DetalheComprovantesComponent', () => {
  let component: DetalheComprovantesComponent;
  let fixture: ComponentFixture<DetalheComprovantesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetalheComprovantesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetalheComprovantesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
