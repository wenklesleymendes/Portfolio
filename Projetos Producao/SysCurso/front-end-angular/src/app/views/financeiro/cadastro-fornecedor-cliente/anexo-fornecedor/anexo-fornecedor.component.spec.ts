import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AnexoFornecedorComponent } from './anexo-fornecedor.component';

describe('AnexoFornecedorComponent', () => {
  let component: AnexoFornecedorComponent;
  let fixture: ComponentFixture<AnexoFornecedorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnexoFornecedorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnexoFornecedorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
