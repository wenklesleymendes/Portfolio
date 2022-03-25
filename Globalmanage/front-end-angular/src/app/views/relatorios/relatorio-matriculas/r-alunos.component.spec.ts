import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RAlunosComponent } from './r-alunos.component';

describe('RAlunosComponent', () => {
  let component: RAlunosComponent;
  let fixture: ComponentFixture<RAlunosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RAlunosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RAlunosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});