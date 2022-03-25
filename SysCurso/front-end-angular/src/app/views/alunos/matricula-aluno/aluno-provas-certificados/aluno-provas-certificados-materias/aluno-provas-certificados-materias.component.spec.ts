/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AlunoProvasCertificadosMateriasComponent } from './aluno-provas-certificados-materias.component';

describe('AlunoProvasCertificadosMateriasComponent', () => {
  let component: AlunoProvasCertificadosMateriasComponent;
  let fixture: ComponentFixture<AlunoProvasCertificadosMateriasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlunoProvasCertificadosMateriasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlunoProvasCertificadosMateriasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
