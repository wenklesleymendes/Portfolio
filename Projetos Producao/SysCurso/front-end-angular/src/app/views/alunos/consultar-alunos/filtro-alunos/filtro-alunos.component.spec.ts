import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FiltroAlunosComponent } from './filtro-alunos.component';

describe('FiltroAlunosComponent', () => {
  let component: FiltroAlunosComponent;
  let fixture: ComponentFixture<FiltroAlunosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FiltroAlunosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FiltroAlunosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
