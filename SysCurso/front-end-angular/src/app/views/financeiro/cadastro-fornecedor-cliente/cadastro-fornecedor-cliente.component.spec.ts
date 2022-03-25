import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroFornecedorClienteComponent } from './cadastro-fornecedor-cliente.component';

describe('CadastroFornecedorClienteComponent', () => {
  let component: CadastroFornecedorClienteComponent;
  let fixture: ComponentFixture<CadastroFornecedorClienteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CadastroFornecedorClienteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CadastroFornecedorClienteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
