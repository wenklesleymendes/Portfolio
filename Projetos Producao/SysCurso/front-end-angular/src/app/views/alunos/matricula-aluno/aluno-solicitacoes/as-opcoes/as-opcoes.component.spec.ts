import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AsOpcoesComponent } from './as-opcoes.component';

describe('AsOpcoesComponent', () => {
  let component: AsOpcoesComponent;
  let fixture: ComponentFixture<AsOpcoesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AsOpcoesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AsOpcoesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
