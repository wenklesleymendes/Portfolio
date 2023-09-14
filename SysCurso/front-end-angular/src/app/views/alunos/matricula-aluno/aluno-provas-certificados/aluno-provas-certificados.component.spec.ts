import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlunoProvasCertificadosComponent } from './aluno-provas-certificados.component';

describe('AlunoProvasCertificadosComponent', () => {
  let component: AlunoProvasCertificadosComponent;
  let fixture: ComponentFixture<AlunoProvasCertificadosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlunoProvasCertificadosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlunoProvasCertificadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
