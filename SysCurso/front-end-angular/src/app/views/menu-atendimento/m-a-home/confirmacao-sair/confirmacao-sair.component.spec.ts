import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmacaoSairComponent } from './confirmacao-sair.component';

describe('ConfirmacaoSairComponent', () => {
  let component: ConfirmacaoSairComponent;
  let fixture: ComponentFixture<ConfirmacaoSairComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfirmacaoSairComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmacaoSairComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
