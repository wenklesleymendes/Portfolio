import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroFornecedorClienteIndividualComponent } from './cadastro-fornecedor-cliente-individual.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from 'src/app/material.module';

describe('CadastroFornecedorClienteIndividualComponent', () => {
  let component: CadastroFornecedorClienteIndividualComponent;
  let fixture: ComponentFixture<CadastroFornecedorClienteIndividualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        ReactiveFormsModule,
        FormsModule,
        MaterialModule
      ],
      declarations: [ CadastroFornecedorClienteIndividualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CadastroFornecedorClienteIndividualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
