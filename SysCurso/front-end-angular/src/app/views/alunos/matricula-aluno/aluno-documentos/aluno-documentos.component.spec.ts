import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlunoDocumentosComponent } from './aluno-documentos.component';

describe('AlunoDocumentosComponent', () => {
  let component: AlunoDocumentosComponent;
  let fixture: ComponentFixture<AlunoDocumentosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlunoDocumentosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlunoDocumentosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
