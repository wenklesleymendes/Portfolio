import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NovoAtendimentoComponent } from './novo-atendimento.component';

describe('NovoAtendimentoComponent', () => {
  let component: NovoAtendimentoComponent;
  let fixture: ComponentFixture<NovoAtendimentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NovoAtendimentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NovoAtendimentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
