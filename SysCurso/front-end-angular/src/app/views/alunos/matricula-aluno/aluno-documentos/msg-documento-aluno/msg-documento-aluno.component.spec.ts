import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MsgDocumentoAlunoComponent } from './msg-documento-aluno.component';

describe('MsgDocumentoAlunoComponent', () => {
  let component: MsgDocumentoAlunoComponent;
  let fixture: ComponentFixture<MsgDocumentoAlunoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MsgDocumentoAlunoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MsgDocumentoAlunoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
