import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlunoPortalParametrizacaoComponent } from './aluno-portal-parametrizacao.component';

describe('AlunoPortalParametrizacaoComponent', () => {
  let component: AlunoPortalParametrizacaoComponent;
  let fixture: ComponentFixture<AlunoPortalParametrizacaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlunoPortalParametrizacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlunoPortalParametrizacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
