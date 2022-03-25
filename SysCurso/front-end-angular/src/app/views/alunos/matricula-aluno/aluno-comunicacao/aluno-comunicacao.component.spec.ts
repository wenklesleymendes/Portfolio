import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlunoComunicacaoComponent } from './aluno-comunicacao.component';

describe('AlunoComunicacaoComponent', () => {
  let component: AlunoComunicacaoComponent;
  let fixture: ComponentFixture<AlunoComunicacaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlunoComunicacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlunoComunicacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
